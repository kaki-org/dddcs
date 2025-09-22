// 姓だけを表示する
var fullName = "kakikubo teruo";
var tokens = fullName.Split(' '); // ["kakikubo", "teruo"]

var lastName = tokens[0];
Console.WriteLine(lastName); // kakikubo が表示される