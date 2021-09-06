using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AI_Project1.Helpers
{
    static class Helper
    {
        private static StringBuilder solutionOutput = new StringBuilder();
        private static int step = 0;
        private static int row = 8;
        private static int col = 8;

        static Helper()
        {
            var lines = File.ReadAllLines("input.txt");
            row = lines.Length;
            var trimmedItems = lines[0].Trim();
            col = trimmedItems.Split(' ').Length;
        }

        public static Node LoadInitialState()
        {
            var i = 0;
            var state = new int[row, col];

            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var trimmedItems = line.Trim();
                var items = trimmedItems.Split(' ');
                var j = 0;
                foreach (var item in items)
                {
                    if (int.TryParse(item, out int itemInt))
                    {
                        state[i, j] = itemInt;
                        j++;
                    }
                    else
                        throw new Exception("Invalid character in the input file.");
                }
                i++;
            }
            return new Node(state);
        }

        public static void SaveSolution(Node goal, int expandedCount, TimeSpan timeElapsed)
        {
            RenderSolution(goal);
            var line1 = $"The Path ({step - 1} move(s) and {expandedCount} node(s) expanded) - Time Elapsed: {timeElapsed.TotalSeconds} seconds.";
            var output = $"{line1}\n\n{solutionOutput.ToString()}\n";
            ExportSolutionToFile(output);
            //Console.WriteLine(output);
            ExportDataToFile(step - 1, expandedCount, timeElapsed.TotalSeconds);
        }

        private static void RenderSolution(Node node)
        {
            if (node == null)
                return;

            RenderSolution(node.Parent);
            var nodeOutput = GetNodeStateString(node);
            solutionOutput.AppendLine($"Step {step} with a heuristic value of {node.HeuristicValue}.");
            solutionOutput.AppendLine(nodeOutput);
            step++;
        }

        private static string GetNodeStateString(Node node)
        {
            var sb = new StringBuilder();
            sb.Append("┌───");
            for (int k = 0; k < node.State.GetLength(1) - 1; k++)
                sb.Append("┬───");
            sb.AppendLine("┐");
            for (int i = 0; i < node.State.GetLength(0); i++)
            {
                for (int j = 0; j < node.State.GetLength(1); j++)
                {
                    if (j == 0)
                        sb.Append("│");

                    var piece = " ";
                    if (node.State[i, j] == 1)
                        piece = "Q"; // ♕
                    else if (node.State[i, j] == 2)
                        piece = "B"; // ♗
                    sb.Append($" {piece} │");
                }
                sb.AppendLine();
                if (i != node.State.GetLength(0) - 1)
                {
                    sb.Append("├───");
                    for (int k = 0; k < node.State.GetLength(1) - 1; k++)
                        sb.Append("┼───");
                    sb.AppendLine("┤");
                }
            }
            sb.Append("└───");
            for (int k = 0; k < node.State.GetLength(1) - 1; k++)
                sb.Append("┴───");
            sb.AppendLine("┘");

            return sb.ToString();
            /*
            ┌───┬───┬───┬───┬───┬───┬───┬───┐
            │ x │ x │ x │ x │ x │ x │ x │ x │
            ├───┼───┼───┼───┼───┼───┼───┼───┤
            │ x │ x │ x │ x │ x │ x │ x │ x │
            └───┴───┴───┴───┴───┴───┴───┴───┘
            */
        }

        private static void ExportSolutionToFile(string output)
        {
            var fileName = "output.txt";
            File.WriteAllText(fileName, output);
            Process.Start("notepad", Path.Combine(Directory.GetCurrentDirectory(), fileName));
        }
        private static void ExportDataToFile(int stepCount, int expandedCount, double timeElapsed)
        {
            var stepCountFileName = "stepCount.txt";
            var expandedCountFileName = "expandedCount.txt";
            var timeElapsedFileName = "timeElapsed.txt";  

            string[] lines = { };
            if (File.Exists(stepCountFileName))
                lines = File.ReadAllLines(stepCountFileName);

            using (StreamWriter writer = new StreamWriter(stepCountFileName, true))
                writer.WriteLine($"{lines.Length}\t{stepCount}");

            using (StreamWriter writer = new StreamWriter(expandedCountFileName, true))
                writer.WriteLine($"{lines.Length}\t{expandedCount}");

            using (StreamWriter writer = new StreamWriter(timeElapsedFileName, true))
                writer.WriteLine($"{lines.Length}\t{timeElapsed}");
        }
    }
}
