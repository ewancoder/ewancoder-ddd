This project is yet another CQRS (featuring DDD, CQRS, ES) framework
implementation created for specific goals at hand. It provides simple yet
powerful bones for applications written with **CQRS**, **DDD** and
**Event Sourcing** in mind.

The project began as a study goal to learn and understand CQRS/Event Sourcing
patterns. There're lots of examples and tutorials of CQRS/ES and many
repositories with sample projects, but almost every project implements
**Event Store** in memory. Hence I decided to implement simple Event Store
ready to use out of the box. Currently the implementation uses Entity Framework
and all events are stored in single table. In the future I am going to include
Greg's EventStore adapter as well.

* Full-featured CQRS and Event Sourcing bare bones for building event sourced
systems. It also should be compatible with CQRS-only systems and ES-only systems.
* Real production EventStore (no in-memory implementation) with serialization
and data persistence (using Entity Framework as of now).
* In future I may include here Ewancoder.Services project which represents
service bus implementation for building microservices architecture.

The project is currently maintained and updated daily, yet it may not be
suitable for full-blown production use (although I do use it in production).