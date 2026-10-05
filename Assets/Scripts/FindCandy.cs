using CustomUtility;

public class FindCandy : AddThingScript<Candy>
{
    protected override void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNullChild(ref collider, gameObject, nameof(collider), ErrorSeverity.Warning);
    }
}