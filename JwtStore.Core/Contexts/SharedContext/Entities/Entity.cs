namespace JwtStore.Core.Contexts.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
  public Guid Id { get; private set; }

  protected Entity() => Id = Guid.NewGuid();

  public bool Equals(Guid id) => Id == id;

  public override int GetHashCode() => Id.GetHashCode();
}