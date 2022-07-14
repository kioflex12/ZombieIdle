namespace Behavior.Gameplay.Crafting
{
    public interface ICraftingListener
    {
        void OnCraftingStageChanged(CraftingStage newCraftingStage);
        void OnCraftingProgressChanged(float newCraftingProgress);
    }
}

