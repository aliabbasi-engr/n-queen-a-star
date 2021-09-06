using AI_Project1.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project1.GoalFinders
{
    class RecursiveGoalFinder : GoalFinder
    {
        private static List<Node> open = new List<Node>();
        private static List<Node> expanded = new List<Node>();

        public override (Node, int) Find(Node node)
        {
            expanded.Add(node);
            Logger.Info("Node added to the expanded list");

            if (node.IsGoal())
                return (node, expanded.Count - 1);

            var successors = node.GetSuccessors();
            foreach (var successor in successors)
            {
                var openItem = open.SingleOrDefault(x => Compare(x.State, successor.State));
                var expandedItem = expanded.SingleOrDefault(x => Compare(x.State, successor.State));

                if (openItem == null && expandedItem == null)
                {
                    open.Add(successor);
                    Logger.Info("Node added to the open list");
                }
                else
                {
                    var evaluation = successor.GetEvaluation();
                    if (openItem != null && evaluation < openItem.GetEvaluation())
                    {
                        open.Remove(openItem);
                        Logger.Info("Node removed from the open list");
                        open.Add(successor);
                        Logger.Info("Successor added to the open list");
                    }
                    else if (expandedItem != null && evaluation < expandedItem.GetEvaluation())
                    {
                        open.Add(successor);
                        Logger.Info("Successor added to the open list");
                        expanded.Remove(expandedItem);
                        Logger.Info("Successor removed from the expanded list");
                    }
                }
            }

            if (open.Count == 0)
                return (null, 0);

            var minEvaluation = open.Min(x => x.GetEvaluation());
            var minNode = open.First(x => x.GetEvaluation() == minEvaluation);

            var minGoal = open.FirstOrDefault(x => x.IsGoal() && x.GetEvaluation() == minEvaluation);
            if (minGoal != null)
                minNode = minGoal;

            open.Remove(minNode);
            Logger.Info("Minimum node removed from the open list");

            return Find(minNode);
        }
    }
}
