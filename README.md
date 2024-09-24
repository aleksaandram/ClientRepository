# ClientRepository
application for client processing


the idea is to use two entities Client and Address who represent 
1.information about the client
2.precise information about the Address

the data is structured in two separate tables in local database.

The relation is 1:N meaning 1 clientID can have N AddressID, which is also reflected in the class with storing the addresses in a list as a property (part of the Client class).


