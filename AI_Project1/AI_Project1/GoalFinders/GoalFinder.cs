namespace AI_Project1.GoalFinders
{
    abstract class GoalFinder
    {
        public abstract (Node, int) Find(Node node);

        protected bool Compare(int[,] state1, int[,] state2)
        {
            for (int i = 0; i < state1.GetLength(0); i++)
            {
                for (int j = 0; j < state1.GetLength(1); j++)
                {
                    if (state1[i, j] != state2[i, j])
                        return false;
                }
            }
            return true;
        }
    }
}
