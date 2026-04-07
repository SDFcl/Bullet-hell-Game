# Character Flow

ไฟล์นี้สรุป flow ของระบบ Character แบบอ่านง่ายก่อนทำ UML แยก `Player` และ `Enemy`

## 1. แกนร่วมของ Character

ทั้ง `Player` และ `Enemy` ใช้ component หลักร่วมกันหลายตัว

- `Movement`
  หน้าที่: รับ direction หรือ input แล้วขยับ Rigidbody2D
- `Health`
  หน้าที่: เก็บ HP, รับ damage, ยิง event ตอนเลือดเปลี่ยนหรือเมื่อตาย
- `Mana`
  หน้าที่: เก็บ mana, regen mana, consume mana ตอนใช้อาวุธบางชนิด
- `Attack`
  หน้าที่: เป็นจุดเริ่มโจมตีของตัวละคร แล้วไปเรียกอาวุธที่ถืออยู่
- `Facing2D`
  หน้าที่: กลับด้านตัวละครซ้าย/ขวา
- `AimPivot2D`
  หน้าที่: หมุน pivot ของอาวุธหรือทิศยิง

สรุปสั้นๆ:

`Character = HP + Mana + Move + Aim + Attack`

---

## 2. Player Flow

`PlayerController` คือศูนย์กลางของฝั่งผู้เล่น

มันไม่ได้ทำทุกอย่างเอง แต่มีหน้าที่ "รับ input แล้วสั่ง component อื่น"

### 2.1 ตอนเริ่มเกม

ใน `Awake()`

- หา `Movement`
- หา `Facing2D`
- หา `AimPivot2D`
- หา `PlayerInput`
- หา `Dodge`
- หา `RayInteract`
- หา `HoldingWeapon`
- หา `Inventory`
- หา `Attack`
- สร้าง `FilterInteract`

ดังนั้นในหัวให้คิดว่า

`PlayerController` = ตัวรวมทุกระบบย่อยของ Player

### 2.2 Flow ตอนเดิน

ลำดับ:

1. Input เรียก `OnMove()`
2. `PlayerController` อ่านค่า movement
3. ส่งค่าไป `Movement.SetMoveInput()`
4. `Movement.FixedUpdate()` ขยับ Rigidbody2D

สรุป:

`Input -> PlayerController -> Movement -> Rigidbody2D`

### 2.3 Flow ตอนเล็ง

ลำดับ:

1. ถ้าเป็น gamepad จะใช้ `OnAim()`
2. ถ้าเป็น keyboard/mouse จะอ่านตำแหน่งเมาส์ใน `Update()`
3. `PlayerController` คำนวณทิศ `dir`
4. ส่ง `dir.x` ไป `Facing2D.SetDirection()`
5. ส่ง `dir` ไป `AimPivot2D.SetDirection()`

สรุป:

`Input/Mouse -> PlayerController -> Facing2D + AimPivot2D`

### 2.4 Flow ตอนยิง

ลำดับ:

1. Input เรียก `OnFire()` หรือ `OnAim()` เพื่อเปิด state ยิง
2. ใน `Update()` ถ้า `isfiring == true`
3. `PlayerController` เรียก `attack.TryAttack()`
4. `Attack` หาอาวุธปัจจุบัน
5. อาวุธ execute การโจมตี

สรุป:

`Input -> PlayerController -> Attack -> Current Weapon`

### 2.5 Flow ตอน dodge

ลำดับ:

1. Input เรียก `OnDodge()`
2. `PlayerController` เรียก `dodge.TryDodge()`
3. `Dodge` ปิด movement ชั่วคราว
4. `Dodge` ใช้ทิศจาก `IMovement.GetDirection()`
5. `Dodge` ใส่แรงให้ Rigidbody2D
6. `PlayerAnimations` ฟัง event `OnDodge` แล้ว trigger animation

สรุป:

`Input -> PlayerController -> Dodge -> Movement/Rigidbody2D`

และ animation flow:

`Dodge.OnDodge -> PlayerAnimations`

### 2.6 Flow ตอน interact

ลำดับ:

1. `RayInteract.Update()` หา target ที่ใกล้ที่สุดตลอด
2. Input เรียก `OnInteract()`
3. `PlayerController` ส่ง `rayInteract.target` เข้า `FilterInteract`
4. `FilterInteract` ตรวจว่า target เป็น
   - `IPickable`
   - หรือ `IInteractive`
5. แล้วค่อยเรียก `Interact(player)`

สรุป:

`Nearby Object -> RayInteract -> PlayerController -> FilterInteract -> Target.Interact()`

### 2.7 Flow ตอนเปลี่ยนอาวุธ

ลำดับ:

1. Input เรียก `OnHoldItem()`
2. `PlayerController` เรียก `HoldingWeapon.SetDirection()`
3. `HoldingWeapon` เลือก index ใหม่จาก `Inventory.Weapons`
4. ลบของที่ถืออยู่เก่า
5. Instantiate ของใหม่จาก `HoldingPrefab`

สรุป:

`Input -> PlayerController -> HoldingWeapon -> Inventory.Weapons`

### 2.8 Flow ตอนใช้ consumable

ลำดับ:

1. Input เรียก `UseConsumable()`
2. `PlayerController` หยิบของชิ้นแรกใน `inventory.Consumables`
3. เรียก `Item.Use()`
4. `Item` วนใช้ `ItemEffect`
5. เอาของออกจาก inventory

สรุป:

`Input -> PlayerController -> Inventory -> Item -> ItemEffect`

---

## 3. Enemy Flow

`EnemyController` คือศูนย์กลางของฝั่งศัตรู

ต่างจาก Player ตรงที่มันไม่ได้รับ input จากผู้เล่น
แต่มันรับ "การตัดสินใจ" จาก State Machine

### 3.1 ตอนเริ่มเกม

ใน `Awake()`

- หา target เป็น player
- สร้าง `EnemyContext`
- เอา component ต่างๆใส่เข้า context
  - `Health`
  - `Mana`
  - `Movement`
  - `AimPivot2D`
  - `Facing2D`
  - `PathToDir`
  - `LineOfSight2D`
  - `Attack`
- สร้าง `StateMachine<EnemyContext>`
- build state graph จาก `EnemyStateGraphSO`
- initialize state machine

สรุป:

`EnemyController -> EnemyContext -> StateMachine`

### 3.2 Runtime flow ของ Enemy

ใน `Update()`

1. `ctx.Timer.Tick()`
2. `stateMachine.Tick()`
3. state machine เช็ค condition
4. ถ้าต้องเปลี่ยน state ก็ change state
5. ถ้าไม่เปลี่ยน ก็เรียก action ของ state ปัจจุบัน

สรุป:

`EnemyController.Update() -> Timer -> StateMachine -> Action/Transition`

### 3.3 EnemyContext มีไว้ทำอะไร

`EnemyContext` คือกล่องรวม dependency ของ enemy

พูดง่ายๆคือแทนที่ action แต่ละตัวจะ `GetComponent` เองทุกครั้ง
ก็ให้มันหยิบจาก context ได้เลย

ตัวอย่างของที่อยู่ใน context:

- `Self`
- `Target`
- `Health`
- `Mana`
- `Movement`
- `Facing2D`
- `AimPivot2D`
- `PathToDir`
- `LineOfSight2D`
- `Attack`
- `Timer`

### 3.4 Flow ตอน Chase

ลำดับ:

1. state machine เข้า `Chase`
2. `EnemyChaseAction` สั่ง `PathToDir.SetDestination(target.position)`
3. `PathToDir` คำนวณทิศเดิน
4. action ส่งทิศให้
   - `Facing2D`
   - `AimPivot2D`
   - `Movement`

สรุป:

`EnemyState(Chase) -> PathToDir -> Facing2D/AimPivot2D/Movement`

### 3.5 Flow ตอน Wander

ลำดับ:

1. state machine เข้า `Wander`
2. สุ่มปลายทางในรัศมีที่กำหนด
3. ส่งปลายทางให้ `PathToDir`
4. เอาทิศที่ได้ไปสั่ง `Movement`
5. ถ้าถึงปลายทางให้รอ
6. ถ้าติด/ค้างให้สุ่มใหม่

สรุป:

`EnemyState(Wander) -> Random Destination -> PathToDir -> Movement`

### 3.6 Flow ตอน Attack

ลำดับ:

1. state machine เข้า `Attack`
2. หยุด movement
3. clear path
4. เมื่อออกจาก state นี้ เรียก `ctx.Attack.TryAttack()`

สรุป:

`EnemyState(Attack) -> stop move -> Attack.TryAttack()`

### 3.7 Flow ตอนตาย

ลำดับ:

1. `Health` ถูกลดจน `IsDead`
2. `OnDead` ถูกยิง
3. condition/state ของ enemy เปลี่ยนไป Dead
4. `EnemyDeadAction` หยุดเดิน
5. ถ้ามี `Drop` ก็ drop item

สรุป:

`Health.OnDead -> Enemy Dead State -> Drop`

---

## 4. ภาพรวมเปรียบเทียบ Player กับ Enemy

### เหมือนกัน

- มี `Health`
- มี `Mana`
- มี `Movement`
- มี `Facing2D`
- มี `AimPivot2D`
- มี `Attack`

### ต่างกัน

Player:

- ขับเคลื่อนด้วย `Input`
- มี `Inventory`
- มี `HoldingWeapon`
- มี `RayInteract`
- มี `FilterInteract`
- มี `Dodge`

Enemy:

- ขับเคลื่อนด้วย `StateMachine`
- มี `EnemyContext`
- มี `PathToDir`
- มี `LineOfSight2D`
- มี `EnemyStateGraphSO`

---

## 5. จำแบบสั้นที่สุด

### Player

`Input -> PlayerController -> Component ย่อย`

### Enemy

`StateMachine -> EnemyController/Context -> Component ย่อย`

### Character Core

`Health + Mana + Movement + Aim + Attack`

