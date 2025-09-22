// うまく姓を表示できないパターン
var fullName = "john smith";
var tokens = fullName.Split(' '); // ["john", "smith"]

var lastName = tokens[0];
Console.WriteLine(lastName); // john が表示される(姓はsmithの筈)