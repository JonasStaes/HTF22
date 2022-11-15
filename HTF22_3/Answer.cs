using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HTF22_3 {

    public class Response {
        public List<int> doors { get; set; }
        public int roomNr { get; set; }
        public bool finished { get; set; }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n\t")
                .AppendLine("\n\tdoors: [");
            doors.ForEach(x => builder.AppendLine($"\t\t{x},"));
            builder.AppendLine("\t],")
                .AppendLine($"\troomNr: {roomNr},")
                .AppendLine($"\tfinished: {finished},");
            return builder.Append("}").ToString();
        }
    }
    public static class Answer {
        public async static Task GetAnswer(Response response, string url, HttpClient client) {

            bool foundArtifact = response.finished;
            List<int> correctChoices = new List<int>();

            while(!foundArtifact) {
                //kies een random door:
                var door = response.doors[new Random().Next(response.doors.Count())];

                var postResponse = await client.PostAsJsonAsync<int>(url, door);
                var postResponseValue = await postResponse.Content.ReadFromJsonAsync<Response>();
                response = postResponseValue;

                if(response.roomNr != 1) {
                    //we zijn verder gegaan, dus we hebben een goede keuze gemaakt
                    correctChoices.Add(door);
                    foundArtifact = response.finished;

                    if(foundArtifact) {
                        Console.WriteLine("We hebben het artifact gevonden!");
                        break;
                    }
                } else {
                    //teruggestuurd naar de eerste kamer, dus we hebben een verkeerde keuze gemaakt
                    //we moeten terug naar de vorige kamer en een andere deur kiezen

                    //om dit te doen, zullen we eerst een loop moeten doen met alle correcte keuzes die we hebben gemaakt
                    //en dan de sample bovenliggende while opnieuw laten lopen.
                    foreach(var correctChoice in correctChoices) {
                        var res = await client.PostAsJsonAsync<int>(url, correctChoice);
                        var resVal = await res.Content.ReadFromJsonAsync<Response>();
                        response = resVal;
                    }
                }
            }
            Console.WriteLine("Correct choices: " + string.Join(", ", correctChoices));
        }
    }
}
