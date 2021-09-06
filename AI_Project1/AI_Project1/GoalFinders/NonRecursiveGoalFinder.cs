using AI_Project1.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project1.GoalFinders
{
    class NonRecursiveGoalFinder : GoalFinder
    {
        private static List<Node> open = new List<Node>();
        private static List<Node> expanded = new List<Node>();

        public override (Node, int) Find(Node initialNode)
        {
            open.Add(initialNode);
            Logger.Info("Initial node added to the open list");

            while (open.Any())
            {
                var minEvaluation = open.Min(x => x.GetEvaluation());
                var minNode = open.First(x => x.GetEvaluation() == minEvaluation);
                
                var minGoal = open.FirstOrDefault(x => x.IsGoal() && x.GetEvaluation() == minEvaluation);
                if (minGoal != null)
                    return (minGoal, expanded.Count + 1);

                open.Remove(minNode);
                Logger.Info("Minimum node removed from the open list");

                var successors = minNode.GetSuccessors();
                foreach (var successor in successors)
                {
                    if (successor.IsGoal())
                        return (successor, expanded.Count + 1);

                    var openItem = open.FirstOrDefault(x => Compare(x.State, successor.State));
                    if (openItem != null && openItem.GetEvaluation() < successor.GetEvaluation())
                        continue;

                    var expandedItem = open.FirstOrDefault(x => Compare(x.State, successor.State));
                    if (expandedItem != null && expandedItem.GetEvaluation() < successor.GetEvaluation())
                        continue;

                    open.Add(successor);
                    Logger.Info("Node added to the open list");
                }

                expanded.Add(minNode);
                Logger.Info("Minimum node added to the expanded list");
            }

            return (null, 0);
        }
    }
}
