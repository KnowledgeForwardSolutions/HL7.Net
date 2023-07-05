namespace HL7.Net.Core;

/// <summary>
///   A composite field containing a person name. Implements HL7 V2.2 spec 
///   section 2.8.10.8.
/// </summary>
public record PersonNameField : IPresence
{
   internal PersonNameField() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification("PN", 1, "Family Name", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 2, "Given Name", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 3, "Middle Name or Initial", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 4, "Suffix", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 5, "Prefix", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 6, "Degree", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
   };

   /// <summary>
   ///   Represents a person name field that is not present.
   /// </summary>
   public static readonly PersonNameField NotPresent = new();

   /// <summary>
   ///   Represents a person name field that is present but is null.
   /// </summary>
   public static readonly PersonNameField PresentButNull = new()
   {
      Presence = Presence.PresentButNull
   };

   /// <inheritdoc/>
   public Presence Presence { get; protected set; } = Presence.NotPresent;

   /// <summary>
   ///   The person's family (last) name.
   /// </summary>
   public StringField FamilyName { get; protected set; } = StringField.NotPresent;

   /// <summary>
   ///   The person's given (first) name.
   /// </summary>
   public StringField GivenName { get; protected set; } = StringField.NotPresent;

   /// <summary>
   ///   The person's middle name or middle initial.
   /// </summary>
   public StringField MiddleNameOrInitial { get; protected set; } = StringField.NotPresent;

   /// <summary>
   ///   The person's name suffix (e.g. JR or III).
   /// </summary>
   public StringField Suffix { get; protected set; } = StringField.NotPresent;

   /// <summary>
   ///   The person's name prefix (e.g. DR).
   /// </summary>
   public StringField Prefix { get; protected set; } = StringField.NotPresent;

   /// <summary>
   ///   The person's degree (e.g. MD).
   /// </summary>
   public StringField Degree { get; protected set; } = StringField.NotPresent;

   internal static PersonNameField Parse(
      ref FieldEnumerator fieldEnumerator,
      EncodingDetails encodingDetails,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return PersonNameField.NotPresent;
      }

      if (fieldEnumerator.Current.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return PersonNameField.PresentButNull;
      }

      var fieldContents = fieldEnumerator.Current.Length > fieldSpecification.Length
         ? fieldEnumerator.Current[..fieldSpecification.Length]
         : fieldEnumerator.Current;
      var componentEnumerator = fieldContents.ToFields(
         encodingDetails.ComponentSeparator,
         encodingDetails.EscapeCharacter);

      var field = new PersonNameField();

      field.FamilyName = StringField.Parse(
         ref componentEnumerator,
         encodingDetails,
         _fieldSpecifications[0],
         lineNumber,
         log);

      field.GivenName = StringField.Parse(
         ref componentEnumerator,
         encodingDetails,
         _fieldSpecifications[1],
         lineNumber,
         log);

      field.MiddleNameOrInitial = StringField.Parse(
         ref componentEnumerator,
         encodingDetails,
         _fieldSpecifications[2],
         lineNumber,
         log);

      field.Suffix = StringField.Parse(
         ref componentEnumerator,
         encodingDetails,
         _fieldSpecifications[3],
         lineNumber,
         log);

      field.Prefix = StringField.Parse(
         ref componentEnumerator,
         encodingDetails,
         _fieldSpecifications[4],
         lineNumber,
         log);

      field.Degree = StringField.Parse(
         ref componentEnumerator,
         encodingDetails,
         _fieldSpecifications[5],
         lineNumber,
         log);

      if (componentEnumerator.MoveNext())
      {
         log.LogWarning(Messages.AdditionalDataIgnored, lineNumber, fieldSpecification);
      }

      field.Presence = Presence.Present;

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldEnumerator.Current, checkTruncated: true);

      return field;
   }
}
