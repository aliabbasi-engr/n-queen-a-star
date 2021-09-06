using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Project1
{
    class Node
    {
        public Node(int[,] state) : this(state, 0) { }

        public Node(int[,] state, int cost) : this(state, cost, null) { }

        public Node(int[,] state, int cost, Node parent)
        {
            State = state;
            Cost = cost;
            Parent = parent;
        }

        public int Cost { get; set; }

        public double HeuristicValue => GetHeuristic();

        public int[,] State { get; set; }

        public Node Parent { get; set; }

        public double GetEvaluation()
        {
            return GetHeuristic() + Cost;
        }

        public bool IsGoal()
        {
            return GetHeuristic() == 0;
        }

        public double GetHeuristic()
        {
            var temp1 = 0.0;
            var temp2 = 0.0;

            var queenQueenThreats = 0.0;
            var queenBishopThreats = 0.0;
            var bishopBishopThreats = 0.0;
            var bishopQueenThreats = 0.0;

            var heuristicValue = 0.0;

            for (int i = 0; i < State.GetLength(0); i++)
            {
                for (int j = 0; j < State.GetLength(1); j++)
                {
                    if (State[i, j] == 1)
                    {
                        (temp1, temp2) = GetThreatCountByQueen(i, j);
                        queenQueenThreats += temp1;
                        queenBishopThreats += temp2;
                    }
                    else if (State[i, j] == 2)
                    {
                        (temp1, temp2) = GetThreatCountByBishop(i, j);
                        bishopBishopThreats += temp1;
                        bishopQueenThreats += temp2;
                    }
                }
            }

            heuristicValue = (queenQueenThreats * (0.5) + queenBishopThreats * (0.5) + bishopBishopThreats * (0.5) + bishopQueenThreats * (0.5));
            return heuristicValue;
        }

        private (int, int) GetThreatCountByQueen(int row, int col)
        {
            var queenQueenThreats = 0;
            var queenBishopThreats = 0;

            // Top
            for (int i = row - 1; IsIndexValid(i, col); i--)
            {
                if (State[i, col] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[i, col] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Bottom
            for (int i = row + 1; IsIndexValid(i, col); i++)
            {
                if (State[i, col] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[i, col] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Left
            for (int j = col - 1; IsIndexValid(row, j); j--)
            {
                if (State[row, j] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[row, j] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Right
            for (int j = col + 1; IsIndexValid(row, j); j++)
            {
                if (State[row, j] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[row, j] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Top-Right
            for (int i = row - 1, j = col + 1; IsIndexValid(i, j); i--, j++)
            {
                if (State[i, j] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Top-Left 
            for (int i = row - 1, j = col - 1; IsIndexValid(i, j); i--, j--)
            {
                if (State[i, j] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Bottom-Right
            for (int i = row + 1, j = col + 1; IsIndexValid(i, j); i++, j++)
            {
                if (State[i, j] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            // Bottom-Left
            for (int i = row + 1, j = col - 1; IsIndexValid(i, j); i++, j--)
            {
                if (State[i, j] == 1)
                {
                    queenQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    queenBishopThreats++;
                    break;
                }
            }

            return (queenQueenThreats, queenBishopThreats);
        }

        private (int, int) GetThreatCountByBishop(int row, int col)
        {
            var bishopBishopThreats = 0;
            var bishopQueenThreats = 0;

            // Top-Right
            for (int i = row - 1, j = col + 1; IsIndexValid(i, j); i--, j++)
            {
                if (State[i, j] == 1)
                {
                    bishopQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    bishopBishopThreats++;
                    break;
                }
            }

            // Top-Left 
            for (int i = row - 1, j = col - 1; IsIndexValid(i, j); i--, j--)
            {
                if (State[i, j] == 1)
                {
                    bishopQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    bishopBishopThreats++;
                    break;
                }
            }

            // Bottom-Right
            for (int i = row + 1, j = col + 1; IsIndexValid(i, j); i++, j++)
            {
                if (State[i, j] == 1)
                {
                    bishopQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    bishopBishopThreats++;
                    break;
                }
            }

            // Bottom-Left
            for (int i = row + 1, j = col - 1; IsIndexValid(i, j); i++, j--)
            {
                if (State[i, j] == 1)
                {
                    bishopQueenThreats++;
                    break;
                }
                else if (State[i, j] == 2)
                {
                    bishopBishopThreats++;
                    break;
                }
            }

            return (bishopQueenThreats, bishopBishopThreats);
        }

        public List<Node> GetSuccessors()
        {
            var nodes = new List<Node>();

            for (int i = 0; i < State.GetLength(0); i++)
            {
                for (int j = 0; j < State.GetLength(1); j++)
                {
                    if (State[i, j] == 1)
                        nodes.AddRange(GetQueenMoveSuccessors(i, j));
                    else if (State[i, j] == 2)
                        nodes.AddRange(GetBishopMoveSuccessors(i, j));
                }
            }

            return nodes;
        }

        private List<Node> GetQueenMoveSuccessors(int row, int col)
        {
            var nodes = new List<Node>();

            // Top
            for (int i = row - 1; IsIndexValid(i, col); i--)
            {
                if (State[i, col] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, col] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Bottom
            for (int i = row + 1; IsIndexValid(i, col); i++)
            {
                if (State[i, col] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, col] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Left
            for (int j = col - 1; IsIndexValid(row, j); j--)
            {
                if (State[row, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[row, j] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Right
            for (int j = col + 1; IsIndexValid(row, j); j++)
            {
                if (State[row, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[row, j] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Top-Right
            for (int i = row - 1, j = col + 1; IsIndexValid(i, j); i--, j++)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Top-Left
            for (int i = row - 1, j = col - 1; IsIndexValid(i, j); i--, j--)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Bottom-Right
            for (int i = row + 1, j = col + 1; IsIndexValid(i, j); i++, j++)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Bottom-Left
            for (int i = row + 1, j = col - 1; IsIndexValid(i, j); i++, j--)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 1
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            return nodes;
        }

        private List<Node> GetBishopMoveSuccessors(int row, int col)
        {
            var nodes = new List<Node>();

            // Top-Right
            for (int i = row - 1, j = col + 1; IsIndexValid(i, j); i--, j++)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 2
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Top-Left
            for (int i = row - 1, j = col - 1; IsIndexValid(i, j); i--, j--)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 2
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Bottom-Right
            for (int i = row + 1, j = col + 1; IsIndexValid(i, j); i++, j++)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 2
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            // Bottom-Left
            for (int i = row + 1, j = col - 1; IsIndexValid(i, j); i++, j--)
            {
                if (State[i, j] != 0)
                    break;

                var newState = (int[,])State.Clone();
                newState[i, j] = State[row, col]; // Always 2
                newState[row, col] = 0;

                var node = new Node(newState, Cost + 1, this);

                nodes.Add(node);
            }

            return nodes;
        }

        private bool IsIndexValid(int i, int j)
        {
            return i >= 0 && i < State.GetLength(0) && j >= 0 && j < State.GetLength(1);
        }
    }
}