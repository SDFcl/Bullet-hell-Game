/*
╔════════════════════════════════════════════════════════════════════════════╗
║                    INTERACTION FILTER SYSTEM GUIDE                         ║
║                          (OOP-Based Design)                                ║
╚════════════════════════════════════════════════════════════════════════════╝

📋 OVERVIEW
═══════════
The new filter system automatically detects and executes interactions based on
component type. It separates two types of interactions:

  1. ✓ PICKABLE (items that can be picked up)
     - Weapons
     - Consumables
     - Coins
     - Any item in inventory

  2. ✓ INTERACTIVE (objects with special actions)
     - Doors
     - Chests
     - NPCs
     - Levers/Switches
     - Anything that requires special interaction logic


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📌 INTERFACES
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

1️⃣ IPickable
─────────────
For anything that can be picked up/collected

  public interface IPickable : IInteractable
  {
      ItemData GetItemData();
  }

  Methods to implement:
  • Interact(GameObject player) - from IInteractable
  • GetItemData() - return the item's data


2️⃣ IInteractive
────────────────
For anything with custom interaction logic

  public interface IInteractive : IInteractable
  {
      string GetInteractionName();    // "Open Door", "Read Sign", etc.
      bool CanInteract(GameObject player);  // Check conditions
  }

  Methods to implement:
  • Interact(GameObject player) - from IInteractable
  • GetInteractionName() - return action name
  • CanInteract(GameObject player) - return true/false


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
💻 HOW TO USE
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Simple Usage (from PlayerController):
─────────────────────────────────────

  // Already initialized in Awake()
  private FilterInteract filterInteract;

  // In Awake()
  filterInteract = new FilterInteract(gameObject);

  // When interact button is pressed
  filterInteract.FilterAndExecute(rayInteract.target);
  
  // That's it! ✓ No need to check types manually


Advanced Usage:
───────────────

  // Check interaction type without executing
  string type = filterInteract.GetInteractionType(target);
  if (type == "Pickable")
  {
      Debug.Log("Will pick up item");
  }


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📝 EXAMPLE 1: Implementing IPickable (Coins)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  public class Coins : MonoBehaviour, IPickable
  {
      public int coinAmount = 10;

      public ItemData GetItemData()
      {
          // Return coin data (or null if not needed)
          return null;
      }

      public void Interact(GameObject player)
      {
          // Add coins to player
          PlayerHealth health = player.GetComponent<PlayerHealth>();
          if (health != null)
          {
              health.AddCoins(coinAmount);
          }
          Destroy(gameObject);
      }
  }


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📝 EXAMPLE 2: Implementing IInteractive (Chest - Random Rewards)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  public class Chest : InteractiveObject
  {
      [SerializeField] private ItemData[] possibleRewards;
      [SerializeField] private int minRewards = 1;
      [SerializeField] private int maxRewards = 3;
      [SerializeField] private bool isOpened = false;
      [SerializeField] private Animator animator;

      public override string GetInteractionName()
      {
          return isOpened ? "Chest (Opened)" : "Open Chest";
      }

      public override bool CanInteract(GameObject player)
      {
          if (isOpened) return false;
          return base.CanInteract(player); // Check distance/key
      }

      protected override void ExecuteInteraction()
      {
          if (isOpened) return;

          isOpened = true;
          animator.SetTrigger("Open");

          // Give random rewards
          int rewardCount = Random.Range(minRewards, maxRewards + 1);
          for (int i = 0; i < rewardCount; i++)
          {
              ItemData reward = possibleRewards[Random.Range(0, possibleRewards.Length)];
              Instantiate(reward.WorldPrefab, transform.position + Vector3.up, Quaternion.identity);
          }
      }
  }


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📝 EXAMPLE 3: Implementing IInteractive (Shop - Buy with Coins)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  public class Shop : InteractiveObject
  {
      [SerializeField] private ItemData[] shopItems;
      [SerializeField] private Transform shopUI;
      [SerializeField] private GameObject itemSlotPrefab;
      [SerializeField] private Transform itemContainer;
      [SerializeField] private TextMeshProUGUI playerCoinsText;

      private Inventory playerInventory;

      public override string GetInteractionName()
      {
          return "Open Shop";
      }

      public override bool CanInteract(GameObject player)
      {
          return true; // Always can interact with shop
      }

      protected override void ExecuteInteraction()
      {
          OpenShop();
      }

      private void OpenShop()
      {
          shopUI.gameObject.SetActive(true);
          Time.timeScale = 0f; // Pause game

          // Find player inventory
          playerInventory = GameObject.FindGameObjectWithTag("Player")
              .GetComponentInChildren<Inventory>();

          UpdateCoinsDisplay();
          PopulateShopItems();
      }

      private void UpdateCoinsDisplay()
      {
          playerCoinsText.text = $"Coins: {playerInventory.Coins}";
      }

      private void PopulateShopItems()
      {
          foreach (Transform child in itemContainer)
              Destroy(child.gameObject);

          foreach (ItemData item in shopItems)
          {
              GameObject slot = Instantiate(itemSlotPrefab, itemContainer);
              ShopItemSlot slotScript = slot.GetComponent<ShopItemSlot>();
              slotScript.Setup(item, this);
          }
      }

      public void TryBuyItem(ItemData itemData)
      {
          if (playerInventory.SpendCoins((int)itemData.Price))
          {
              Item newItem = new Item(itemData);
              if (itemData.itemType == ItemType.Weapon)
                  playerInventory.AddWeapon(newItem);
              else
                  playerInventory.AddConsumable(newItem);

              UpdateCoinsDisplay();
          }
      }
  }


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🔍 FLOW DIAGRAM
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  Player presses Interact button
        ↓
  OnInteract() called
        ↓
  FilterAndExecute(rayInteract.target) called
        ↓
        ├─→ Has IPickable? ─→ YES ─→ ExecutePickup() ✓
        │
        └─→ Has IInteractive? ─→ YES ─→ CanInteract()? ─→ YES ─→ ExecuteInteractive() ✓
                                                             ↓
                                                            NO → Log Warning
        ↓
    No valid interface found → Log Error


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
✨ BENEFITS
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  ✓ Auto-detection: Filter automatically determines interaction type
  ✓ Simple API: Just call filterInteract.FilterAndExecute(target)
  ✓ Extensible: Easy to add new interaction types (just implement interface)
  ✓ Type-safe: Compile-time checking with interfaces
  ✓ Single Responsibility: Each class handles one interaction type
  ✓ Open/Closed Principle: Open for extension, closed for modification
  ✓ Reusable: Can add IPickable/IInteractive to any GameObject


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🎯 CHECKLIST: Adding a New Interaction Type
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  If it can be PICKED UP:
    ☐ Make class inherit from IPickable
    ☐ Implement GetItemData()
    ☐ Implement Interact(GameObject player)
    ☐ Add component to prefab/GameObject

  If it's an INTERACTIVE OBJECT:
    ☐ Make class inherit from IInteractive
    ☐ Implement GetInteractionName()
    ☐ Implement CanInteract(GameObject player)
    ☐ Implement Interact(GameObject player)
    ☐ Add component to prefab/GameObject

  That's it! FilterInteract will automatically handle it ✓

*/

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🎯 SETUP GUIDE: Creating a Shop
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  1. Create empty GameObject → Add Shop component
  2. Create Shop UI Canvas:
     - Panel (shopUI)
       - TextMeshPro (playerCoinsText)
       - Vertical Layout Group (itemContainer)
       - Button (closeButton)
  3. Create Item Slot Prefab:
     - Image (itemIcon)
     - TextMeshPro (itemNameText)
     - TextMeshPro (itemPriceText)
     - Button (buyButton) → Add ShopItemSlot component
  4. Assign references in Shop component
  5. Add ItemData assets to shopItems array


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🎯 SETUP GUIDE: Creating a Chest
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  1. Create GameObject → Add Chest component
  2. Add Animator component (optional)
  3. Create ItemData assets for rewards
  4. Assign possibleRewards array
  5. Set minRewards/maxRewards (e.g., 1-3)
  6. Add chest opening animation (optional)


━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📝 EXAMPLE 4: ShopItemSlot Component
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

  // Attach this to your shop item slot prefab
  public class ShopItemSlot : MonoBehaviour
  {
      [SerializeField] private Image itemIcon;
      [SerializeField] private TextMeshProUGUI itemNameText;
      [SerializeField] private TextMeshProUGUI itemPriceText;
      [SerializeField] private Button buyButton;

      private ItemData itemData;
      private Shop shop;

      public void Setup(ItemData data, Shop shopReference)
      {
          itemData = data;
          shop = shopReference;

          itemIcon.sprite = data.itemIcon;
          itemNameText.text = data.itemName;
          itemPriceText.text = $"{data.Price} coins";

          buyButton.onClick.AddListener(() => shop.TryBuyItem(itemData));
      }
  }
