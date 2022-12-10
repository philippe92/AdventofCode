using System;
using System.IO;

namespace AdventOfCode
{
    class Jour18_2020 : IJour
    {
        string[] formulas;
        public void Init(string inputfile)
        {
             formulas = File.ReadAllLines(inputfile);
        }

        private long CalculateValueOrParenthesis(string formula, ref int offset, bool hasAdditionPriority)
        {
            long result;
            if (formula[offset] == '(')
            {
                offset++;
                result = CalculateFormula(formula, ref offset, hasAdditionPriority);
                offset++;
            }
            else
            {
                result = long.Parse(formula[offset].ToString());
                offset++;
            }
            return result;
        }

        public long CalculateFormula(string formula, ref int offset, bool hasAdditionPriority)
        {
            long value = CalculateValueOrParenthesis(formula, ref offset, hasAdditionPriority);
           
            while (offset < formula.Length && formula[offset] != ')')
            {
                offset++;
                char operand = formula[offset];
                offset++;
                offset++;

                long rvalue;
                
                if (operand == '+' || !hasAdditionPriority)
                {
                    rvalue = CalculateValueOrParenthesis(formula, ref offset, hasAdditionPriority);
                }
                else
                {
                    rvalue = CalculateFormula(formula, ref offset, hasAdditionPriority);
                }
                
                if (operand == '+')
                {
                    value += rvalue;
                }
                else
                {
                    value *= rvalue;
                }
            }

            return value;
        }

        public void ResolveFirstPart()
        {
            long result = 0;
            for (int i = 0; i < formulas.Length; i++)
            {
                int offset = 0;
                result += CalculateFormula(formulas[i], ref offset, false);
            }
            Console.WriteLine($"Resultat = {result}");
        }

        public void ResolveSecondPart()
        {
            long result = 0;
            for (int i = 0; i < formulas.Length; i++)
            {
                int offset = 0;
                result += CalculateFormula(formulas[i], ref offset, true);
            }
            Console.WriteLine($"Resultat = {result}");
        }
    }
}
