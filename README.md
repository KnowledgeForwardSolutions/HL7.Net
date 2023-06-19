# HL7.Net

HL7.Net is a .Net library for reading and producing text based HL7 messages. 
HL7.Net has two primary use cases: reading HL7 text messages to produce a strongly
typed object graph that can be consumed by your business process and the creation
of a strongly typed object graph that can be render as an HL7 text message. The
details of the HL7 specification (field/component separators, escaped values
and more) is handled behind the scenes for you allowing you to focus on your
business processes. 

The HL7.Net object graph is immutable. It is simply an intermediate form between
an HL7 text message and your own business model(s). For example:

  HL7 (text) -> HL7.Net object graph -> your business model(s)

or

  Your business model(s) -> HL7.Net object graph -> HL7 (text)
