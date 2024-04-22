﻿using System.Text.Json;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

public static class UserSeedFactory
{
	private const string UserAsJson = """
	                                  [
	                                    {
	                                  	"Id": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
	                                  	"FirstName": "John",
	                                  	"LastName": "Doe",
	                                  	"Email": "286848@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScgIcP58MKd4CpuMNwdTncyXrDwBGmCYXWHA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "6f6e4b5a-0114-4be6-892c-a8fe015d702a",
	                                  	"FirstName": "Abdel",
	                                  	"LastName": "Abbott",
	                                  	"Email": "325031@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQnPBYGnizEPaBF0QwOfW8VwaKLhW7l_BNZ-g\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "c979bc9c-cef2-47e8-901d-177293a326ce",
	                                  	"FirstName": "Abdiel",
	                                  	"LastName": "Abel",
	                                  	"Email": "282837@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScgIcP58MKd4CpuMNwdTncyXrDwBGmCYXWHA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "74d098dc-069a-4a14-8841-65bec15c1e3e",
	                                  	"FirstName": "Abdul",
	                                  	"LastName": "Acevedo",
	                                  	"Email": "338814@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScgIcP58MKd4CpuMNwdTncyXrDwBGmCYXWHA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "4c7441dc-0af3-415b-af4e-f8f96178f038",
	                                  	"FirstName": "Abdullah",
	                                  	"LastName": "Acosta",
	                                  	"Email": "302294@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSc9w4Q2_5f1MPMOkMVFKAGpQKiAd1FDXEmAA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "2a086b6b-f7e7-45eb-bb02-e9e9fe568472",
	                                  	"FirstName": "Abdulrahman",
	                                  	"LastName": "Adams",
	                                  	"Email": "298834@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSc9w4Q2_5f1MPMOkMVFKAGpQKiAd1FDXEmAA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "53645f68-409e-4cab-9028-dc799a27dc61",
	                                  	"FirstName": "Abel",
	                                  	"LastName": "Adkins",
	                                  	"Email": "337654@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTb9aNaL9NPigrPE2RHc0pdFwW2kCpLdXLQlg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
	                                  	"FirstName": "Abelardo",
	                                  	"LastName": "Aguilar",
	                                  	"Email": "332728@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTVXVQsEgS-4o8fKXeH8OCtDoNhutVZsxMKYg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "e4e3ceab-603d-4299-89c6-266655232ff5",
	                                  	"FirstName": "Abhiram",
	                                  	"LastName": "Aguirre",
	                                  	"Email": "270144@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpH6lqX6Fw6nvefHPtQhdzQweq-6nrQoPRkw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "31bf665d-7112-4c6a-80b4-6bfba7ce1f96",
	                                  	"FirstName": "Abner",
	                                  	"LastName": "Akers",
	                                  	"Email": "322736@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8Z9nqTcJCSbriwHevvzheseOZz-bJCl_WRg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "3e0e493c-df3d-4a4e-a068-bc9c53562f42",
	                                  	"FirstName": "Abraham",
	                                  	"LastName": "Akins",
	                                  	"Email": "333153@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTf8F6UcXWjKbVrhN-ASIADQ4Y6AUd5mG1VLQ\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "f27723e5-da0b-4d23-94dd-a1805729bf63",
	                                  	"FirstName": "Abram",
	                                  	"LastName": "Alexander",
	                                  	"Email": "336214@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQGhe47t5HIHR2yDCSxI3QnU9lYom_Lzevxqg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "caece75d-37c7-4b68-882f-998662456889",
	                                  	"FirstName": "Ace",
	                                  	"LastName": "Ali",
	                                  	"Email": "314543@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR1nYidgLj-y0UPVsNI1pW0cb345mkN19xJnw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "da804bb6-c593-4ce1-ab84-6fc10302ec53",
	                                  	"FirstName": "Achilles",
	                                  	"LastName": "Allen",
	                                  	"Email": "311653@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQybj4XXcmV9MqOQmZqm-H0CLdWWMwS60YKUA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "4f4a7b99-2e70-494e-ae96-7636391a860d",
	                                  	"FirstName": "Adam",
	                                  	"LastName": "Allender",
	                                  	"Email": "317262@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_Upg5hSLUdAHImorOh4FCnToNmlf9rEjjxA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "1fa83e26-0270-4998-b0bb-d36ef2f3e764",
	                                  	"FirstName": "Adan",
	                                  	"LastName": "Allison",
	                                  	"Email": "296471@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQW8nB5qGvWfkEvmuLX0zhPR0kREBiiRXjQHjvZcIWPbVePZe0TWdK3NIVCzrZFLQL9i_Q\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "bc997748-d3f9-47af-b9b5-d74dc89a8ae4",
	                                  	"FirstName": "Addison",
	                                  	"LastName": "Allman",
	                                  	"Email": "315187@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQETnSBdcbAQEequVc0wjGaXweBwsvP01gmyBoKoGIVD9uBUkCetVR5DRDBdrFfCBawFFY\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "24394c39-ce83-49f6-81a2-0b2e70d81438",
	                                  	"FirstName": "Aden",
	                                  	"LastName": "Alvarado",
	                                  	"Email": "324475@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOCcQpl5fOtkEmJ9LJYqHSDy3pB073bsyqCQ\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "503457fb-dd03-40f6-8e89-79aee51a8736",
	                                  	"FirstName": "Adhvik",
	                                  	"LastName": "Alvarez",
	                                  	"Email": "304041@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAePHGk4zQacrlExygB4QUQlmSmCR9Qxd1Sw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "6d374401-8b62-4635-b83d-e7954c772d0b",
	                                  	"FirstName": "Adiel",
	                                  	"LastName": "Andersen",
	                                  	"Email": "291881@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRbw2JJA1v4Ca4XXuIZHaNPkADBUUHFc4Aadg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
	                                  	"FirstName": "Aditya",
	                                  	"LastName": "Anderson",
	                                  	"Email": "283879@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSSIFjdTdLCtrzQ94BV5GznGQmWpM9xXek5rw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "8c67e577-4e52-4276-a707-5b8693b2f2cc",
	                                  	"FirstName": "Adler",
	                                  	"LastName": "Andrade",
	                                  	"Email": "289371@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSSvs0JoTlLXy0QqZfCua0W5yf02DYtm-CxKg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "0310a038-576e-4b98-b0e6-b67647db1be3",
	                                  	"FirstName": "Adolfo",
	                                  	"LastName": "Andrews",
	                                  	"Email": "315301@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTBDxyQKhrJ1rV3IYY-fuegWTDd3F7xKHpkw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "ad494675-60b0-40d5-bcf0-f0e611393a18",
	                                  	"FirstName": "Adonai",
	                                  	"LastName": "Anthony",
	                                  	"Email": "270192@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-akrx4MUpbaPYm6dVDPvgCVvRMHpsQaNmUw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "46f255d5-3769-4c48-87a8-c288c0bbc468",
	                                  	"FirstName": "Aadhya",
	                                  	"LastName": "Aragon",
	                                  	"Email": "272039@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQoahyqaFgzPSd6xnPMCisAGWNvOLrpTJwvSA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "e9f8ed4a-0d86-4ca0-bc77-17f0b90186e6",
	                                  	"FirstName": "Aadya",
	                                  	"LastName": "Archer",
	                                  	"Email": "319353@via.dk",
	                                  	"Avatar": "https://media.istockphoto.com/id/1332100919/vector/man-icon-black-icon-person-symbol.jpg?s=612x612\u0026w=0\u0026k=20\u0026c=AVVJkvxQQCuBhawHrUhDRTCeNQ3Jgt0K1tXjJsFy1eg="
	                                    },
	                                    {
	                                  	"Id": "0f785ef3-73d7-4446-859c-d11612a9b029",
	                                  	"FirstName": "Aahana",
	                                  	"LastName": "Arellano",
	                                  	"Email": "333269@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQuxeno-P9psRhxIeXEQdz2J7mtx6wUmwMd1g\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "549e0299-d85b-49ad-8d02-098c8e1b46d1",
	                                  	"FirstName": "Aaleyah",
	                                  	"LastName": "Arias",
	                                  	"Email": "310477@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQuxeno-P9psRhxIeXEQdz2J7mtx6wUmwMd1g\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "747c376d-2736-4837-a74f-53bb259dc6dd",
	                                  	"FirstName": "Aaliyah",
	                                  	"LastName": "Armstrong",
	                                  	"Email": "287389@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSIxbBnC6_8zQ2CYwdcksyDnGhK9v4idRyPgQ\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "3bdd82ed-ce96-4bb7-89a5-5ab4e3239be7",
	                                  	"FirstName": "Aanya",
	                                  	"LastName": "Arnold",
	                                  	"Email": "276739@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWtV8MMACHa8rdN3sq-c8oTKmkYzvLua3_A_9YvNaGW5OjHWj5TgxCJr5Y8Gi9cS2t97Y\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "c42e316a-6260-440b-ae0f-2359b7ba6f77",
	                                  	"FirstName": "Aarna",
	                                  	"LastName": "Arroyo",
	                                  	"Email": "337454@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRhNoCSP7vKiYm_nDBLmCMvRaaBRL78rlJpP4lVy6nVJoQcOQ27dG_m3ZWx7TKINOpERA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
	                                  	"FirstName": "Aarohi",
	                                  	"LastName": "Ashley",
	                                  	"Email": "274832@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBhtSPnmjr09ERciIU5qjGNMvcXnlNOcJOYA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "c80bb627-7a86-467c-91b8-ae48daf477e3",
	                                  	"FirstName": "Aarya",
	                                  	"LastName": "Ashton",
	                                  	"Email": "302937@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRNBITwGofqT4yuvJQ2C1N_M5_ahx9WF5B9Tg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
	                                  	"FirstName": "Aavya",
	                                  	"LastName": "Atkins",
	                                  	"Email": "321949@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0m1pzVYtUbyE59cPCDf_y2oZPNxskCnhZHA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "2b25b567-576c-400d-bf37-c3be60d5be07",
	                                  	"FirstName": "Abbey",
	                                  	"LastName": "Atkinson",
	                                  	"Email": "280485@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8aOh9WQoLI-mxG2a_SL609HsHkgWmV9s0dA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "954931e0-88ca-4598-ab70-290be22e16b5",
	                                  	"FirstName": "Abbie",
	                                  	"LastName": "Austin",
	                                  	"Email": "289359@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ_B4dHzhqmPqFfpNYtr2xb0clABZxs2Hb12w\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "dc04ecf4-1ccd-41f8-afc1-bbc209f83c0a",
	                                  	"FirstName": "Abbigail",
	                                  	"LastName": "Avery",
	                                  	"Email": "275705@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0gLiga9QTkuqpMkbTARN0LTzwDJQH0yixMw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "2e31b742-5826-49da-8a1f-784ea0473e02",
	                                  	"FirstName": "Abby",
	                                  	"LastName": "Avila",
	                                  	"Email": "325598@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS8BxnJvfe-jW3MSctMfM3mkkVk5RbhE4Khfg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "46fc609c-ac4a-4186-9864-a635293f09a8",
	                                  	"FirstName": "Abbygail",
	                                  	"LastName": "Ayala",
	                                  	"Email": "331746@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRg4LAqlO0MWJt_12uP6ZCMTv5zqEpWtY9aSw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "86a87d88-93bf-404f-a5b0-5d60a5cf19b7",
	                                  	"FirstName": "Abigail",
	                                  	"LastName": "Ayers",
	                                  	"Email": "304337@via.dk",
	                                  	   "Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUVK9yvSbMFUsBdYZsEx4EyIjn_rTWb1kQwg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "c6ab5431-402c-4ffc-b43b-df2318ab5cc9",
	                                  	"FirstName": "Abigale",
	                                  	"LastName": "Bailey",
	                                  	"Email": "334576@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUVK9yvSbMFUsBdYZsEx4EyIjn_rTWb1kQwg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "ad0d8dc3-2608-4989-a079-6decaee8241a",
	                                  	"FirstName": "Abriella",
	                                  	"LastName": "Baird",
	                                  	"Email": "301728@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTbpF3IRjq3K2vF74PNI4mpc-kzYwXmegSupg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "082efad5-e113-4aee-9c15-4ba25d4fd03d",
	                                  	"FirstName": "Abrielle",
	                                  	"LastName": "Baker",
	                                  	"Email": "334102@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpH6lqX6Fw6nvefHPtQhdzQweq-6nrQoPRkw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "e3b7c26a-dd07-4324-92da-a238d46e6ead",
	                                  	"FirstName": "Abril",
	                                  	"LastName": "Baldwin",
	                                  	"Email": "292266@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8Z9nqTcJCSbriwHevvzheseOZz-bJCl_WRg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
	                                  	"FirstName": "Abygail",
	                                  	"LastName": "Ball",
	                                  	"Email": "278391@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTf8F6UcXWjKbVrhN-ASIADQ4Y6AUd5mG1VLQ\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "d69f9348-8da0-4077-8cf9-14fb20595a76",
	                                  	"FirstName": "Acacia",
	                                  	"LastName": "Alvarado",
	                                  	"Email": "330213@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQGhe47t5HIHR2yDCSxI3QnU9lYom_Lzevxqg\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "2dff5453-f686-4faf-8323-74ad56c97b1a",
	                                  	"FirstName": "Abraham",
	                                  	"LastName": "Andrade",
	                                  	"Email": "284401@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR1nYidgLj-y0UPVsNI1pW0cb345mkN19xJnw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "9d267736-3444-4648-a08e-ee466ddefe33",
	                                  	"FirstName": "Adiel",
	                                  	"LastName": "Arellano",
	                                  	"Email": "277368@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQybj4XXcmV9MqOQmZqm-H0CLdWWMwS60YKUA\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "2e09e219-f919-46a8-91f7-e5a48874480b",
	                                  	"FirstName": "Aanya",
	                                  	"LastName": "Alvarado",
	                                  	"Email": "283529@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQAePHGk4zQacrlExygB4QUQlmSmCR9Qxd1Sw\u0026usqp=CAU"
	                                    },
	                                    {
	                                  	"Id": "5893604a-5eff-46c4-8056-77161a6e9665",
	                                  	"FirstName": "Abby",
	                                  	"LastName": "Armstrong",
	                                  	"Email": "295895@via.dk",
	                                  	"Avatar": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRbw2JJA1v4Ca4XXuIZHaNPkADBUUHFc4Aadg\u0026usqp=CAU"
	                                    }
	                                  ]
	                                  """;

	public static List<User> CreateUsers()
	{
		List<TmpUser> userTmps = JsonSerializer.Deserialize<List<TmpUser>>(UserAsJson)!;

		return userTmps.Select(x => new User()
		{
			Id = x.Id,
			FirstName = x.FirstName,
			LastName = x.LastName,
			Email = x.Email,
			Avatar = x.Avatar
		}).ToList();
	}

	private record TmpUser(string Id, string FirstName, string LastName, string Email, string Avatar);

}