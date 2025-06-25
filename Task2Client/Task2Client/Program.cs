//Program.cs
var cnt = new HttpClient();
//base address uri is from task 2
cnt.BaseAddress = new Uri("https://localhost:7054");
var content = new StringContent("X=3&Y=56", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
var msg = cnt.PostAsync("/api/Math", content).Result;

if (msg.StatusCode == System.Net.HttpStatusCode.OK)
{
    var result = msg.Content.ReadAsStringAsync().Result;
    Console.WriteLine(result);
}