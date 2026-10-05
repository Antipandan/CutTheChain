using CustomUtility;

public class FindCandy : AddThingScript<Candy>
{
    #region Custom Methods
    
    protected override void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNullChild(ref collider, gameObject, nameof(collider), ErrorSeverity.Warning);
    }
    #endregion
}