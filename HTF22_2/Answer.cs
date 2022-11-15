using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTF22_2 {

    public struct Wizard {
        public int strength { get; set; }
        public int speed { get; set; }
        public int health { get; set; }

        public override string ToString() {
            return "{ " + $"strength: {strength}, speed: {speed}, health: {health}"+" }";
        }
    }

    public struct Teams {
        public List<Wizard> teamA { get; set; }
        public List<Wizard> teamB { get; set; }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n\t")
                .AppendLine("\n\ta: [");
            teamA.ForEach(x => builder.AppendLine($"\t\t{x},"));
            builder.AppendLine("\t],\n\tb: [");
            teamB.ForEach(x => builder.AppendLine($"\t\t{x},"));
            builder.AppendLine("\t]");
            return builder.Append("}").ToString();
        }
    }

    public static class Answer {

        public static string GetAnswer(Teams teams) {
            var teamA = teams.teamA;
            var teamB = teams.teamB;
            while(true) {
                //Nakijken of er een team is met 0 wizards
                if(teamA.Count == 0 || teamB.Count == 0) {
                    break;
                }

                foreach(Wizard wizard in teamA) {
                    foreach(Wizard target in teamB) {
                        while(wizard.health > 0 || target.health > 0) {
                            if(target.speed < wizard.speed)
                                target.health -= wizard.strength;
                            else
                                wizard.health -= target.strength;

                            if(target.health <= 0) {
                                break;
                            } else if(wizard.health <= 0) {
                                break;
                            }
                        }
                    }
                }

                teamA.RemoveAll(w => w.health <= 0);
                teamB.RemoveAll(w => w.health <= 0);
            }

            return teamA.Count > 0 ? "TeamA" : "TeamB";
        }
    }
}
