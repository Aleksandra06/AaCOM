namespace Fractions
{
    public class Solutions_Jordan_Gauss
    {
        public delegate void ExampleHandler(string message);
        public event ExampleHandler Notify;
        public bool Solutions_Jordan_Gauss_Metod(MatrixFractions matrix)
        {
            //if (matrix.M - 1 != matrix.N) { if (Notify != null) Notify($"Решение систем линейных уравнений методом Жордана-Гаусса.\nЭтот метод не подходит. Попробуйте другой!\n"); return false; }
            Rectangle rectangle = new Rectangle();
            rectangle.Notify += Message;
            var flag = rectangle.RectangleMetod(matrix);
            if (!flag) { if (Notify != null) Notify($"Метод прямоугольников выполнен не успешно или было найдено противоречие!\n"); return false; }
            //var answer = Аnswer(matrix);
            if (Notify != null) { PrintAnswer(matrix); }
            return true;
        }
        private void PrintAnswer(/*List<Tuple<string, SimpleFractions>> answer*/MatrixFractions matrix)
        {
            if (Notify == null) return;
            SimpleFractionsMeneger sfm = new SimpleFractionsMeneger();
            bool checkX;
            int countX;
            for (int strNum = 0, colNum = 0; strNum < matrix.N || colNum < matrix.N; strNum++, colNum++)
            {
                checkX = false;
                countX = 0;
                for (int i = 0; i < matrix.M - 1; i++)
                {
                    if (matrix.Matrix[strNum, i].Numerator != 0)
                    {
                        if (colNum == i)
                            checkX = true; // обычный случай
                        countX++;
                    }
                }
                if (countX == 1 && checkX == true) // обычный случай
                {
                    SimpleFractions simpleAnswer = new SimpleFractions();
                    simpleAnswer = sfm.Division(matrix.Matrix[strNum, matrix.M - 1], matrix.Matrix[strNum, colNum]);
                    Notify($"x{colNum + 1} = {simpleAnswer.toString()}\n");
                }
                else if (countX == 0) // зануленная строка
                {
                    if (matrix.Matrix[strNum, matrix.M - 1].Numerator == 0)
                        Notify($"x{colNum + 1} - любое\n");
                    else
                        Notify($"Что-то пошло не так\n");
                }
                else if (countX > 1 && checkX == true)//когда в строке остались еще не зануленные х
                {
                    string strAnswer = $"x{colNum + 1} = ";
                    if (matrix.Matrix[strNum, colNum].Numerator != matrix.Matrix[strNum, colNum].Denominator)
                        strAnswer += "( ";
                    if (matrix.Matrix[strNum, matrix.M - 1].Numerator != 0)
                        strAnswer += $" {matrix.Matrix[strNum, matrix.M - 1].toString()}";
                    int idCol = matrix.N;
                    while (idCol < matrix.M - 1)
                    {
                        if (matrix.Matrix[strNum, idCol].Numerator != 0)
                        {
                            if (matrix.Matrix[strNum, idCol].Numerator == matrix.Matrix[strNum, idCol].Denominator)
                            {
                                strAnswer += $" - x{colNum + 1}";
                            }
                            else
                            {
                                strAnswer += $" - ({matrix.Matrix[strNum, idCol].toString()})*x{idCol + 1}";
                            }
                        }
                        idCol++;
                    }
                    /*SimpleFractions simpleAnswer = new SimpleFractions();
                    simpleAnswer = sfm.Division(matrix.Matrix[strNum, matrix.M - 1], matrix.Matrix[strNum, idCol]);
                    strAnswer += $"{simpleAnswer.toString()}";*/

                    if (matrix.Matrix[strNum, colNum].Numerator != matrix.Matrix[strNum, colNum].Denominator)
                        strAnswer += $" ) / ( {matrix.Matrix[strNum, colNum].toString()} )";
                    Notify($"{strAnswer}\n");
                }
                else
                {
                    Notify($"Это не было предусмотрено(\n");
                }
            }
            //if (Notify == null) return;
            //foreach (var a in answer)
            //{
            //    if (a.Item2 != null)
            //        Notify($"{a.Item1} = {a.Item2.toString()}\n");
            //    else
            //        Notify($"{a.Item1} - любое\n");
            //}
        }
        //private List<Tuple<string, SimpleFractions>> Аnswer(MatrixFractions matrix)
        //{
        //    SimpleFractionsMeneger sfm = new SimpleFractionsMeneger();
        //    List<Tuple<string, SimpleFractions>> answer = new List<Tuple<string, SimpleFractions>>();
        //    for (int strNum = 0; strNum < matrix.N; strNum++)
        //    {
        //        if (matrix.Matrix[strNum, strNum].Numerator != 0)
        //        {
        //            SimpleFractions simpleAnswer = new SimpleFractions();
        //            simpleAnswer = sfm.Division(matrix.Matrix[strNum, matrix.M - 1], matrix.Matrix[strNum, strNum]);
        //            answer.Add(new Tuple<string, SimpleFractions>(item1: "x" + (strNum + 1).ToString(), item2: simpleAnswer));
        //        }
        //        else
        //        {
        //            answer.Add(new Tuple<string, SimpleFractions>(item1: "x" + (strNum + 1).ToString(), item2: null));
        //        }
        //    }
        //    return answer;
        //}
        private void Message(string message)
        {
            if (Notify != null) Notify(message);
        }
    }
}
