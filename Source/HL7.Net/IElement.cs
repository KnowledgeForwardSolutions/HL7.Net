namespace HL7.Net;

/// <summary>
///   An field, component or subcomponent is present in a segment or composite 
///   datatype.
/// </summary>
public interface IElement
{
   /// <summary>
   ///   Identifies if the field, component or subcomponent is present, present
   ///   but null or not present.
   /// </summary>
   Presence Presence { get; }
}
