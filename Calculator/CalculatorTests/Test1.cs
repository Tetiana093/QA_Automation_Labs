using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using AnalaizerClassLibrary;

namespace CalculatorTests
{
    [TestClass]
    public class Variant6Tests
    {
        
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\2курс\тестування\Calculator\CalculatorTests\CalcTestDB.mdf;Integrated Security=True";

        [TestMethod]
        public void IsOperator_Test()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Symbol, Expected FROM IsOperatorTests", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string symbol = reader["Symbol"].ToString();
                        bool expected = Convert.ToBoolean(reader["Expected"]);

                        bool actual = AnalaizerClass.IsOperator(symbol);
                        Assert.AreEqual(expected, actual, $"Помилка для символу: {symbol}");
                    }
                }
            }
        }

        [TestMethod]
        public void InsertSymbol_Test()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT InputString, SymbolToInsert, InsertPosition, ExpectedString FROM InsertSymbolTests", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string inputString = reader["InputString"].ToString();
                        char symbolToInsert = reader["SymbolToInsert"].ToString()[0];
                        int insertPosition = Convert.ToInt32(reader["InsertPosition"]);
                        string expectedString = reader["ExpectedString"].ToString();

                        string actualString = AnalaizerClass.InsertSymbol(inputString, symbolToInsert, insertPosition);
                        Assert.AreEqual(expectedString, actualString, $"Помилка вставки {symbolToInsert} у {inputString}");
                    }
                }
            }
        }
    }
}