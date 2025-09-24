﻿// FullNameクラスのLastNameプロパティを利用する
var fullName = new FullName("teruo", "kakikubo", "");
Console.WriteLine(fullName.LastName); // kakikuboが表示される

// 確実に姓を表示できる
fullName = new FullName("john", "smith", "thomas");
Console.WriteLine(fullName.LastName); // smithが表示される

// 数字の変更
var num = 0;
num = 1;
// 文字の変更
var c = '0';
c = 'b';
// 文字列の変更
var greet = "こんにちは";
Console.WriteLine(greet); // こんにちは　が表示される
greet = "hello";
Console.WriteLine(greet); // hello が表示される
// 値オブジェクトの変更
var fullName2 = new FullName("teruo", "kakikubo", "");
Console.WriteLine(fullName2); // kakikuboが表示される

// 同じ種類の値同士の比較
Console.WriteLine(0 == 0); // True
Console.WriteLine(0 == 1); // False
Console.WriteLine('a' == 'a'); // True
Console.WriteLine('a' == 'b'); // False
Console.WriteLine("hello" == "hello"); // True
Console.WriteLine("hello" == "こんにちは"); // False
// 値オブジェクト同士の比較
var nameA = new FullName("John", "Smith", "Thomas");
var nameB = new FullName("John", "Smith", "Thomas");
// 別個のインスタンス同士の比較
Console.WriteLine("nameAとnameBのEqualsの比較" + nameA.Equals(nameB)); // Trueになる
// 演算子のオーバーライド機能を活用することも選択肢に入る
var compareResult2 = nameA == nameB;
Console.WriteLine(compareResult2);

// 通貨クラスを作成して加算処理を実施する
var myMoney = new Money(1000, "JPY");
var allowance = new Money(3000, "JPY");
Console.WriteLine(myMoney.Add(allowance));
// プリミティブな値同士の計算
var myMoneyP = 1000m;
var allowanceP = 3000m;
Console.WriteLine(myMoneyP + allowanceP);
