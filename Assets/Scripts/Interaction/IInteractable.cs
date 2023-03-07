public interface IInteractable
{
    string Name { get; }
    string InteractionBehaviour { get; }
    bool CanInteract(Interactor interactor);
    void Interact(Interactor interactor);
}