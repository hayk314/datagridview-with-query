/*
  Author: Hayk Aleksanyan
  email:  hayk.aleksanyan@gmail.com
  web:    https://www.github.com/hayk314
 */

using System.Windows.Forms;

namespace DataGridView_withQuery
{
    public static class StaticFunctions
    {
        //container for static methods and global constants  of the project

        #region "string related function"

        public static bool IsSubstring(string strLarge, string strFind, bool exactMatch)
        {
            //true, if strFind is a substring of strLarge

            try
            {
                if (exactMatch)
                {
                    return strLarge == strFind;
                }

                return strLarge.ToLower().Contains(strFind.ToLower());
            }
            catch
            { return false; }
        }

        public static bool IsSubstring(string strLarge, string[] strFind)
        {
            //true, if any member of the array strFind is a substring of strLarge

            try
            {
                for (int i = 0; i < strFind.Length; i++)
                {
                    if (strLarge.ToLower().Contains(strFind[i].ToLower()))
                        return true;
                }

                return false;
            }

            catch
            { return false; }

        }

        public static int Min3(int a, int b, int c)
        {
            // returns min of 3 integers
            if (a < b)
                return a < c ? a : c;
            else
                return b < c ? b : c;
        }

        public static int EditDistance(string word1, string word2)
        {
            // returns the Edit distance between 2 words;
            // the most straightforward implementation; needs improvements

            int n = word1.Length;
            int m = word2.Length;

            if (n == 0 || m == 0)
                return (n < m ? m : n);

            int[][] dp = new int[n + 1][];
            for (int i = 0; i < n + 1; i++)
                dp[i] = new int[m + 1];

            for (int i = 0; i < n + 1; i++)
                dp[i][0] = i;
            for (int i = 0; i < m + 1; i++)
                dp[0][i] = i;

            for (int i = 1; i < n + 1; i++)
                for (int j = 1; j < m + 1; j++)
                {
                    if (word1[i - 1] == word2[j - 1])
                        dp[i][j] = dp[i - 1][j - 1];
                    else
                        dp[i][j] = 1 + Min3(dp[i - 1][j - 1], dp[i - 1][j], dp[i][j - 1]);
                }

            return dp[n][m];
        }

        #endregion

        public static void PopulateOperatorComboBox(ComboBox cmb, string valueType)
        {
            if (cmb is null)
                return;

            if (valueType == Constants.ValueType_Bool)  // Boolean TYPE
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
            }
            else if (valueType == Constants.ValueType_Date)   // DateTime TYPE
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
                cmb.Items.Add(Constants.operator_GEq);
                cmb.Items.Add(Constants.operator_LEq);
                cmb.Items.Add(Constants.operator_Between);
            }
            else if (valueType == Constants.ValueType_Numeric)
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
                cmb.Items.Add(Constants.operator_GEq);
                cmb.Items.Add(Constants.operator_LEq);
                cmb.Items.Add(Constants.operator_Between);
            }
            else //treat as string
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
                cmb.Items.Add(Constants.operator_Like);
                cmb.Items.Add(Constants.operator_Edit_Distance);
            }
        }

        public static void PopulateOperatorComboBox(DataGridViewComboBoxCell cmb, string valueType)
        {
            if (cmb is null)
                return;

            if (valueType == Constants.ValueType_Bool)  // Boolean TYPE
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
            }
            else if (valueType == Constants.ValueType_Date)   // DateTime TYPE
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
                cmb.Items.Add(Constants.operator_GEq);
                cmb.Items.Add(Constants.operator_LEq);
                cmb.Items.Add(Constants.operator_Between);
            }
            else if (valueType == Constants.ValueType_Numeric)
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
                cmb.Items.Add(Constants.operator_GEq);
                cmb.Items.Add(Constants.operator_LEq);
                cmb.Items.Add(Constants.operator_Between);
            }
            else //treat as string
            {
                cmb.Items.Add(Constants.operator_Eq);
                cmb.Items.Add(Constants.operator_NotEq);
                cmb.Items.Add(Constants.operator_Like);
                cmb.Items.Add(Constants.operator_Edit_Distance);
            }

        }

    }
}
