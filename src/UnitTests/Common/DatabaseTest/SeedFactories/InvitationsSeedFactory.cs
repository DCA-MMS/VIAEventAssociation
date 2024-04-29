using System.Text.Json;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Infrastructure.EfcQueries;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace Tests.Common.DatabaseTest.SeedFactories;

public class InvitationsSeedFactory
{
    private const string InvitationsAsJson = """
                                             [
                                               {
                                                 "Id": "53e00e0b-0e58-493b-baf5-c8e4a8331d5c",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "d61f3315-be24-45c0-a374-746a3a46dcdb",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "4c7441dc-0af3-415b-af4e-f8f96178f038",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "be0c4d25-2353-4611-a558-fdfc810fa1b3",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "dc04ecf4-1ccd-41f8-afc1-bbc209f83c0a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "99d32388-7f8a-4e71-b462-297840ef4e4d",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "549e0299-d85b-49ad-8d02-098c8e1b46d1",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "61aa8633-9143-47aa-bb0f-3f40c48668fc",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "5403162f-731b-4bd1-9810-43ae515e5ca8",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "082efad5-e113-4aee-9c15-4ba25d4fd03d",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "1b1808c0-b818-4372-8951-3c9853bd58bf",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "78070aaa-08ff-4724-a318-8d8284f3382c",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "da804bb6-c593-4ce1-ab84-6fc10302ec53",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "dc919158-fa2a-4407-beef-8ecda84bef05",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "0df319ec-6dc5-4c19-86b1-55948b391715",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "e9f8ed4a-0d86-4ca0-bc77-17f0b90186e6",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a1fc65db-3275-49ac-82ce-655487c4967a",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "6f6e4b5a-0114-4be6-892c-a8fe015d702a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "653cd4bc-a90d-4abf-801f-e81a67f9f781",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "2a086b6b-f7e7-45eb-bb02-e9e9fe568472",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "97bd4e95-7835-4ec2-8869-795becffc949",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "e3b7c26a-dd07-4324-92da-a238d46e6ead",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "c3661a5b-1604-4f06-a920-a0ccfb651e9c",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "8c67e577-4e52-4276-a707-5b8693b2f2cc",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "fbc0f19c-455f-4bb1-92ca-ff08aa78f89c",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "ad0d8dc3-2608-4989-a079-6decaee8241a",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "99371dc2-8728-4915-b3ec-3f1f47f2eb05",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7c08f2c5-c488-4c3f-8847-d7f1c188cc4c",
                                                 "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                 "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7fc9989f-97f8-4140-8800-bcbeea839859",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "e3b7c26a-dd07-4324-92da-a238d46e6ead",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "36948ed6-cf0e-438d-a915-3d4e1feb51bc",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "20ac203e-c8dd-4ba3-ab5e-e554730774c3",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "7cb40405-3999-4155-a0ab-9fabf4020dd7",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "ad494675-60b0-40d5-bcf0-f0e611393a18",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "61fbad5c-8511-4e7a-82e4-5f976b72e9ae",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "24394c39-ce83-49f6-81a2-0b2e70d81438",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "b850c4ab-270a-4e5c-b55e-11459f1114bd",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "6d374401-8b62-4635-b83d-e7954c772d0b",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "ccbee38b-8d2f-4328-8ce4-7df4b78a3379",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "3e0e493c-df3d-4a4e-a068-bc9c53562f42",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "c5f0506c-56ba-40ee-8a43-ef244c338b17",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "e9f8ed4a-0d86-4ca0-bc77-17f0b90186e6",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "05479265-793c-458f-b8c9-054a868f84c6",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "0f785ef3-73d7-4446-859c-d11612a9b029",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "3a7ec12f-2978-4cca-a2c0-a085a0303c8c",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "1fa83e26-0270-4998-b0bb-d36ef2f3e764",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "1ff9c063-f798-425a-a1f9-895da3e67d0c",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "550dfb54-552c-47ec-9d7c-1a96dba74ff9",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "bee3c107-3055-4ccc-b59d-a1e5850cbcdc",
                                                 "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                 "GuestId": "86a87d88-93bf-404f-a5b0-5d60a5cf19b7",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "812cdcda-4208-443e-8d9f-22b32652a676",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "ab7c674e-5db9-4aae-aa99-07822ac8769d",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "dc04ecf4-1ccd-41f8-afc1-bbc209f83c0a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "13ea9e6f-38ff-49fa-a3e8-d802548308c4",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "53645f68-409e-4cab-9028-dc799a27dc61",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "bff7d7f1-946c-4035-973d-7f4423721bb4",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "549e0299-d85b-49ad-8d02-098c8e1b46d1",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "eff4d693-3b31-4c3c-9dd4-38207596beed",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "747c376d-2736-4837-a74f-53bb259dc6dd",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "bdf0f4d2-4ee5-4dc7-8c9a-3a6471332af1",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "c42e316a-6260-440b-ae0f-2359b7ba6f77",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "e9487c40-f2d7-4732-8774-572077f9f302",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "3bdd82ed-ce96-4bb7-89a5-5ab4e3239be7",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "f94b0e04-c1d3-49e1-bf64-5a16cd2f0d27",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "4f4a7b99-2e70-494e-ae96-7636391a860d",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "5f3db394-129d-42fa-81c1-8d6ff62d704a",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "c6ab5431-402c-4ffc-b43b-df2318ab5cc9",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "9c8eba6c-3cf7-4490-aa14-0b43d49531ef",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "5893604a-5eff-46c4-8056-77161a6e9665",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "2a33cd33-6dde-4ce4-9840-9717e016dcb0",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "987a21ec-e7bc-40e7-be97-601378a3d1a4",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "e9f8ed4a-0d86-4ca0-bc77-17f0b90186e6",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "d6814a11-ad20-4159-afa9-a5e0e0e56408",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "24394c39-ce83-49f6-81a2-0b2e70d81438",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "95014d3c-4161-4eae-aed0-3975cf848077",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "6f6e4b5a-0114-4be6-892c-a8fe015d702a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "b0cf43df-49f3-4c82-86cc-abbccaea57e0",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "caece75d-37c7-4b68-882f-998662456889",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "a99130f4-0559-4065-957d-ed1d6c373105",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "74d098dc-069a-4a14-8841-65bec15c1e3e",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "083122c1-921d-4696-97f5-945a8d49fd0c",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "3e0e493c-df3d-4a4e-a068-bc9c53562f42",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "deeec37f-0070-421c-89ba-27f643576ac9",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "e4e3ceab-603d-4299-89c6-266655232ff5",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "f4c435c8-54dd-47b7-833b-854874760da2",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "57f7339f-f785-4d96-b37c-ef9da56d5d1e",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "8c67e577-4e52-4276-a707-5b8693b2f2cc",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "aca1a853-0d85-4384-a9d4-c19e3c3ef443",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "da804bb6-c593-4ce1-ab84-6fc10302ec53",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "22b854f1-7e95-47c4-acb8-bdc4bc383e9d",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "c80bb627-7a86-467c-91b8-ae48daf477e3",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a82fed9a-98bb-4208-b6ea-8ed11015263c",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "6d374401-8b62-4635-b83d-e7954c772d0b",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7ede4761-9c8a-4c10-9d72-3a8ac0a61036",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "0f785ef3-73d7-4446-859c-d11612a9b029",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "fdce9c27-6fd0-4358-a99c-e6d253d0ed22",
                                                 "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                 "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "b1317283-1ca5-4458-bdc5-9be46fd0c61a",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "b03e3b10-bdb0-4bf9-ac8e-eb0219a81514",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "0f785ef3-73d7-4446-859c-d11612a9b029",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "3337c7c0-c7be-40d0-916e-f42427fcad8d",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "dc04ecf4-1ccd-41f8-afc1-bbc209f83c0a",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "5b76fc00-0335-45a0-82d1-9236fc65c1b4",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "24394c39-ce83-49f6-81a2-0b2e70d81438",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "3fd2b92c-b036-4509-8b9f-f00bd7040bb4",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "1d411eee-b4b2-4230-91ce-04d638e1bbe3",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "ad0d8dc3-2608-4989-a079-6decaee8241a",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "9df13566-d0c5-445e-85e1-00b5ea5c2df3",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "da804bb6-c593-4ce1-ab84-6fc10302ec53",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "0819c361-43e8-4483-bb04-a82f066d1d22",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "549e0299-d85b-49ad-8d02-098c8e1b46d1",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "61bbf475-c1f7-4284-93b2-d4eb83e829d2",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "caece75d-37c7-4b68-882f-998662456889",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "fe3f7877-4b99-443b-a76b-74198086ec7c",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "e9f8ed4a-0d86-4ca0-bc77-17f0b90186e6",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a8419e7a-8512-4c46-ae71-d41f236ec2d7",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "53645f68-409e-4cab-9028-dc799a27dc61",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "c95ec88c-9730-4935-ae44-41599e1c7fa3",
                                                 "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                 "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "cffe9c45-b580-4b11-a81d-16314d39c057",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "4c7441dc-0af3-415b-af4e-f8f96178f038",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "856daee6-00c7-4ee1-9e36-3d1f7956b5f9",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "5893604a-5eff-46c4-8056-77161a6e9665",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "e356ad30-687e-4b39-bafb-07a173f116df",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "caece75d-37c7-4b68-882f-998662456889",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "db2e44b2-511f-4f3c-acee-b0cc0acf2fbe",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "2a086b6b-f7e7-45eb-bb02-e9e9fe568472",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "ded8e154-0f5b-4617-915d-01105015db66",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "e9f8ed4a-0d86-4ca0-bc77-17f0b90186e6",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "42c439dd-8296-4e5a-9fb9-b92438d5506c",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "c80bb627-7a86-467c-91b8-ae48daf477e3",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "951b272a-4c24-40f8-979d-fca679e47465",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "e3b7c26a-dd07-4324-92da-a238d46e6ead",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "714be87b-00b1-415c-8a72-a304f21023f5",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "9d267736-3444-4648-a08e-ee466ddefe33",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "784919ed-0c1c-4298-b212-ed64ca723743",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "c979bc9c-cef2-47e8-901d-177293a326ce",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "bf5b4df2-127e-4300-ad2b-5e28e675acf0",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "dbffd18e-6896-41ea-9103-6fbf9c820f1c",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "3e0e493c-df3d-4a4e-a068-bc9c53562f42",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a52c31b1-b9ec-4793-8c78-5e13babf0938",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "81c14d2c-ac21-4403-a722-7b6d204e4605",
                                                 "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                 "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "b35fa9a5-a531-484d-8815-854b5706a6a7",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "4f4a7b99-2e70-494e-ae96-7636391a860d",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "e224fae2-ff84-4726-8503-530279870c51",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "8c67e577-4e52-4276-a707-5b8693b2f2cc",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7bdf6c7c-5d76-40a4-9540-f167219eb1d0",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "082efad5-e113-4aee-9c15-4ba25d4fd03d",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "382680b3-5c94-402b-8f8a-59abddab599b",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "53645f68-409e-4cab-9028-dc799a27dc61",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "94690432-4e66-4408-a465-599bc0706877",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "4c7441dc-0af3-415b-af4e-f8f96178f038",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "222357bc-234f-4bd1-857c-ec7239c9aa5b",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "caece75d-37c7-4b68-882f-998662456889",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "457a783d-49aa-4fc1-beed-99b565e7b395",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "24394c39-ce83-49f6-81a2-0b2e70d81438",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a6cfbe2e-6d86-4df0-8fe5-088a8f6a7696",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "549e0299-d85b-49ad-8d02-098c8e1b46d1",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "331449e0-7703-4c93-b996-7bb9e58b6d55",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "c80bb627-7a86-467c-91b8-ae48daf477e3",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "fb00ee7e-3911-4c1f-9966-5df4347d3a34",
                                                 "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                 "GuestId": "747c376d-2736-4837-a74f-53bb259dc6dd",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "9e17b12e-7c8b-4284-bebe-c782dc2021fd",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "6f6e4b5a-0114-4be6-892c-a8fe015d702a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "d21ba5fe-9ebd-42c8-b6ed-0907a6697e87",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "77e57e5d-b975-4e0c-a0f2-f32d7a128b67",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "bc997748-d3f9-47af-b9b5-d74dc89a8ae4",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "25a808c1-5ad2-4553-9d6c-331ef3877a84",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "74d098dc-069a-4a14-8841-65bec15c1e3e",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "fd980c4b-b435-4ba1-ac4d-e4fd732a3668",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "46fc609c-ac4a-4186-9864-a635293f09a8",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "349ffb14-9fcc-41ce-bbd7-4ec272c1586b",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "53645f68-409e-4cab-9028-dc799a27dc61",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "257ff97d-2cb8-44cf-8fe7-2abec5ff0c1c",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "caece75d-37c7-4b68-882f-998662456889",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "acdf50fa-5c37-4743-9344-96196b34ca5f",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "62d7e3c1-c003-452c-94f4-fe6b8f4988cd",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "da804bb6-c593-4ce1-ab84-6fc10302ec53",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "a3ddf928-f01a-4752-96ae-b9bba9be22d7",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "6d374401-8b62-4635-b83d-e7954c772d0b",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "6d115ad7-a7d4-42b7-8ebe-6cf6334ec6a0",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "1fa83e26-0270-4998-b0bb-d36ef2f3e764",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "f3e1d2eb-c183-40de-9fa3-ba57a79a4b34",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "24394c39-ce83-49f6-81a2-0b2e70d81438",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "b9fed6d7-97e5-4faf-9b13-68c9041f9231",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "8c67e577-4e52-4276-a707-5b8693b2f2cc",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "11e995f0-0988-48c6-a1db-586c2505be2b",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "3bdd82ed-ce96-4bb7-89a5-5ab4e3239be7",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "f1b81199-9851-4bbf-8a3b-945cd193b89e",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "493ddb34-dcab-40ed-a87a-99c110c2e2cb",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "9d267736-3444-4648-a08e-ee466ddefe33",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7f76022a-08f9-4245-b9d6-660fc2522c4d",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "d69f9348-8da0-4077-8cf9-14fb20595a76",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "491b8f46-232c-4f6b-be06-1348c7022c2b",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "575f0c65-34ba-477b-9e8a-46eb63f4c499",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "4c7441dc-0af3-415b-af4e-f8f96178f038",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "187b3182-bc07-4560-8c29-b07cd2bd84bb",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "2b25b567-576c-400d-bf37-c3be60d5be07",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "3ca7f36c-86a3-47e1-a880-bd156c4012d3",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "f2df8a83-4919-4bb8-9a08-9bb63ac98db4",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "747c376d-2736-4837-a74f-53bb259dc6dd",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "1598c92a-2f65-4506-9329-a40e9c181185",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "31bf665d-7112-4c6a-80b4-6bfba7ce1f96",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "a0841046-6cf6-4941-b351-8469a1ac3beb",
                                                 "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                 "GuestId": "ad494675-60b0-40d5-bcf0-f0e611393a18",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "d5f5841b-8d24-420d-83e7-b36bd093d734",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "3b49fcc4-8046-4160-8282-ad86f70add4a",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "c80bb627-7a86-467c-91b8-ae48daf477e3",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "0ee73f45-fbcf-430e-a8d3-33d2ac447c0f",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "df00c7b6-4e99-42e6-8993-67d1a61a410d",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "bc997748-d3f9-47af-b9b5-d74dc89a8ae4",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "a51d0bcc-aa1e-47d1-88b6-8e22e0758f5b",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "2a086b6b-f7e7-45eb-bb02-e9e9fe568472",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "88d1b891-2622-42c5-b310-1ce057e9db31",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "c6ab5431-402c-4ffc-b43b-df2318ab5cc9",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7178b8b1-9acd-497b-9dee-a4559d5ee321",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "3bdd82ed-ce96-4bb7-89a5-5ab4e3239be7",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "72738868-44d2-4b2f-bb7d-e63070af3753",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "ad0d8dc3-2608-4989-a079-6decaee8241a",
                                                 "Status": "Pending"
                                               },
                                               {
                                                 "Id": "dcbfdd4c-5e57-4f01-aee4-6d369768421c",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "7f8299a8-663d-45cb-b885-0056ff7ff81f",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "f57cf3d6-fc7a-4066-8bb1-7a1c773ea6ad",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "53645f68-409e-4cab-9028-dc799a27dc61",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "66b94a88-22bf-4818-bf1d-51141d006892",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "0f785ef3-73d7-4446-859c-d11612a9b029",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "eda339c3-ce8c-42ed-91b9-bfeee17fd93c",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "86a87d88-93bf-404f-a5b0-5d60a5cf19b7",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a6305110-313b-4215-83a0-5ac2a79d8275",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "9d267736-3444-4648-a08e-ee466ddefe33",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "1ac9e319-571a-4f69-bfdf-2f0727f6864e",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "ea8054d0-55ef-4a59-82b7-9ec51c720d9d",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "3e0e493c-df3d-4a4e-a068-bc9c53562f42",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "e6a29fc5-e9e3-4dff-b5e8-b6b3d42d40ce",
                                                 "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                 "GuestId": "c42e316a-6260-440b-ae0f-2359b7ba6f77",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "6d01a475-4e68-4261-a45a-306696202124",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "2a086b6b-f7e7-45eb-bb02-e9e9fe568472",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "ffbff65b-bfc6-4a5f-b828-f538e89ba30d",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "3bdd82ed-ce96-4bb7-89a5-5ab4e3239be7",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "d430b58c-4c34-4791-97fd-6d0be3aa8ac9",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "24394c39-ce83-49f6-81a2-0b2e70d81438",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "9fffa68e-77af-45b5-866a-2e49094e4594",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "d69f9348-8da0-4077-8cf9-14fb20595a76",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "f427baff-df92-4a96-a766-3f0d7af8415e",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "c6ab5431-402c-4ffc-b43b-df2318ab5cc9",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "92fb42e5-af96-435b-a8d4-db7866818ff2",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "4f4a7b99-2e70-494e-ae96-7636391a860d",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "8008b12a-c2b2-4670-9a71-6f7c613a93c0",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "86a87d88-93bf-404f-a5b0-5d60a5cf19b7",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "a23d5b9a-e570-4997-a688-8d6521b8cd8b",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "ad0d8dc3-2608-4989-a079-6decaee8241a",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "3620634f-adfa-4568-85c8-0da8b72bff12",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "ad494675-60b0-40d5-bcf0-f0e611393a18",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "64b5bf2b-473a-4711-83a3-2eb5745d10de",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "0a6eba00-78a3-44d5-8223-083bbcfe27db",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "503457fb-dd03-40f6-8e89-79aee51a8736",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "0508579b-9f77-4b87-a6cf-1f48b487e9a5",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "c80bb627-7a86-467c-91b8-ae48daf477e3",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "72b272c9-76e9-400c-b6e6-369b632729ee",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "4c7441dc-0af3-415b-af4e-f8f96178f038",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "2d73ee6f-48bb-4d6e-90b4-853c9aad78ea",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "1fa83e26-0270-4998-b0bb-d36ef2f3e764",
                                                 "Status": "Declined"
                                               },
                                               {
                                                 "Id": "09e1e2ef-dfcd-4193-9797-f977dde4124e",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "c42e316a-6260-440b-ae0f-2359b7ba6f77",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "29d2d3fa-fc4d-4793-a77b-aff5699e0d07",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "8c67e577-4e52-4276-a707-5b8693b2f2cc",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "ecbbdb28-af11-4dea-970e-80efe620c3c2",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "74d098dc-069a-4a14-8841-65bec15c1e3e",
                                                 "Status": "Accepted"
                                               },
                                               {
                                                 "Id": "589ea597-b5bb-4705-a507-7277085ec8ae",
                                                 "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                 "GuestId": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
                                                 "Status": "Declined"
                                               }
                                             ]
                                             """;

    
    private static List<TempInvitation> GetInvitations()
    {
        var tempInvitations = JsonSerializer.Deserialize<List<TempInvitation>>(InvitationsAsJson);

        return tempInvitations;
    }
    
    private static void AddInvitations(VeadatabaseContext context, string eventId)
    {
        var invitations = GetInvitations();
        
        var @event = context.Events.Find(eventId);
        
        foreach (var invitation in invitations)
        {
            var guest = context.Users.Find(invitation.GuestId);
            var status = (int) Enum.Parse<InvitationStatus>(invitation.Status);

            if (@event == null)
              throw new Exception("Event  not found");

            if (guest == null)
              throw new Exception("Guest not found");
            
            var newInvitation = new Invitation
            {
                Id = Guid.NewGuid().ToString(),
                Event = @event,
                Guest = guest,
                Status = status
            };
            
            context.Invitations.Add(newInvitation);
        }
    }
    
    public static void Seed(VeadatabaseContext context)
    {
      var eventIds = context.Events.Select(e => e.Id).ToList();
      
      foreach (var eventId in eventIds)
      {
        AddInvitations(context, eventId);
      }
    }
    
    private record TempInvitation(string EventId, string GuestId, string Status);
}