using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarkParser
{
    public class MarkParser
    {
        static string[] toArray(string line, HashSet<string> commands)
        {
            var list = new List<string>();
            var curString = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                if (commands.Contains(line[i].ToString()) && (i == 0 || line[i - 1] != '\\'))
                {
                    list.Add(curString.ToString());
                    curString.Clear();
                    if (i < line.Length - 1 && line[i + 1] == line[i])
                    {

                        list.Add(line[i].ToString() + line[i + 1].ToString());
                        i++;
                    }
                    else
                        list.Add(line[i].ToString());
                }
                else
                {
                    if (i > 0 && line[i - 1] == '\\' )
                        curString.Remove(curString.Length - 1, 1);
                    curString.Append(line[i].ToString());
                }
            }
            if (curString.Length != 0)
                list.Add(curString.ToString());
            return list.ToArray();
        }

        private static string[] WithoutRepeat(string[] line, HashSet<string> commands)
        {
            var globalStack = new Stack<string>();
            var stackWithCommands = new Stack<string>();
            var i = 0;
            while (i < line.Length)
            {
                if (commands.Contains(line[i]))
                    if (stackWithCommands.Contains(line[i]))
                    {
                        if (stackWithCommands.Peek() != line[i])
                            while (stackWithCommands.Peek() != line[i])
                            {
                                stackWithCommands.Pop();
                                var r = globalStack.Pop();
                                var t = globalStack.Pop();
                                var e = globalStack.Pop();
                                globalStack.Push(e + t + r);
                            }
                        else
                        {
                            stackWithCommands.Pop();
                        }
                    }
                    else
                        stackWithCommands.Push(line[i].ToString());
                globalStack.Push(line[i]);
                i++;
            }
            var list = new List<string>();
            while (globalStack.Count > 0)
            {
                list.Add(globalStack.Pop());
            }
            list.Reverse();
            return list.ToArray();
        }

        static string[] FindLevels(string[] lines, HashSet<string> commands)
        {
            var flagEm = false;
            var list = new List<string>();
            for (var i = 0; i < lines.Length; i++)
            {
                if (commands.Contains(lines[i]))
                {
                    if (lines[i] == "_")
                    {
                        if (flagEm)
                            list.Add("</em>");
                        else
                            list.Add("<em>");
                        flagEm = !flagEm;
                    }
                    if (lines[i] == "__")
                    {
                        if (flagEm)
                            list.Add("</strong>");
                        else
                            list.Add("<strong>");
                        flagEm = !flagEm;
                    }
                }
                else
                    list.Add(lines[i]);
            }
            return list.ToArray();
        }

        public static string ParseMarkToHtml(string lines)
        {
            if (lines.Length == 0)
                return "";
            var commands = new HashSet<string>() {"_", "__"};
            var TagCommands = new HashSet<string>() { "<em>", "</em>" ,"<strong>", "</strong>"};
            var ar = toArray(lines, commands);
            var p = WithoutRepeat(ar, commands);
            foreach (var e in ar)
            {
                Console.WriteLine(e + 9);
            }
            var w = FindLevels(p, commands);
            var u = new StringBuilder();
            var n = new StringBuilder();
            u.Append("<p>\n");
            for (int i = 0; i < w.Length; i++)
            {
                if (w[i].Contains("\n\n"))
                {
                    n.Clear();
                    n.Append(w[i][0]);
                    for (int j = 1; j < w[i].Length; j++)
                    {
                        if (w[i][j] == '\n' && w[i][j - 1] == '\n')
                        {
                            n.Remove(n.Length - 1, 1);
                            n.Append("\n</p>\n<p>\n");
                        }
                        else
                            n.Append(w[i][j]);
                    }
                    u.Append(n + "\n");
                }
                else if (TagCommands.Contains(w[i]))
                {
                    u.Remove(u.Length - 1, 1);
                    u.Append(w[i]);
                }
                else
                {
                    u.Append(w[i] + "\n");
                }
            }
            if (u[u.Length - 1] != '\n')
                u.Append('\n');
            u.Append("</p>");
            return u.ToString();
        }

    }
}
