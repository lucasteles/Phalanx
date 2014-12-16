using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using System.IO;

//using System.Data;
using System.Windows.Forms;

namespace Phalanx.Common
{
    /// <summary>
    /// Extensões dos objetos
    /// </summary>
    public static class Extensions
    {


        /// <summary>
        /// Returns an Object with the specified Type and whose value is equivalent to the specified object.
        /// </summary>
        /// <param name="value">An Object that implements the IConvertible interface.</param>
        /// <param name="conversionType">The Type to which value is to be converted.</param>
        /// <returns>An object whose Type is conversionType (or conversionType's underlying type if conversionType
        /// is Nullable&lt;&gt;) and whose value is equivalent to value. -or- a null reference, if value is a null
        /// reference and conversionType is not a value type.</returns>
        /// <remarks>
        /// This method exists as a workaround to System.Convert.ChangeType(Object, Type) which does not handle
        /// nullables as of version 2.0 (2.0.50727.42) of the .NET Framework. The idea is that this method will
        /// be deleted once Convert.ChangeType is updated in a future version of the .NET Framework to handle
        /// nullable types, so we want this to behave as closely to Convert.ChangeType as possible.
        /// This method was written by Peter Johnson at:
        /// http://aspalliance.com/author.aspx?uId=1026.
        /// </remarks>
        static public object ChangeType(this Object value, Type T)
        {
            if (T == null)
                throw new ArgumentNullException("conversionType");

            if (T.IsGenericType && T.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;

                NullableConverter nullableConverter = new NullableConverter(T);
                T = nullableConverter.UnderlyingType;
            }

            string culture = "pt-BR";
     
            return Convert.ChangeType(value, T, new System.Globalization.CultureInfo(culture));
        }

        /// <summary>
        /// Converte para decimal o valor do objeto
        /// </summary>
        /// <param name="data">Object Data</param>
        /// <exception cref="System.InvalidCastException">Exceção é gerada se objeto contiver algo que não um decimal</exception>
        /// <example>HTTP.Request("DECIMAIS").ToDecimal(0)</example>   
        /// <returns>Decimal</returns>
        static public Decimal ToDecimal(this Object data)
        {
            return Convert.ToDecimal(data);
        }

        /// <summary>
        /// Tenta converter para decimal o valor do objeto
        /// </summary>
        /// <param name="data">Object Data</param>
        /// <param name="Default">Valor default caso valor passado seja vazio ou nulo</param>
        /// <exception cref="System.InvalidCastException">Exceção é gerada se objeto contiver algo que não um decimal</exception>
        /// <example>HTTP.Request("DECIMAIS").ToDecimal(0)</example>   
        /// <returns>Decimal</returns>
        static public Decimal ToDecimal(this Object data, decimal Default)
        {
            if (data == null)
                return Default;
            else if (data == DBNull.Value)
                return Default;

            else if (data.ToString().Trim() == "")
                return Default;
            else
                return Convert.ToDecimal(data);
        }

  
        static public bool Compare<TKey, TValue>(
            this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
                {
                    if (first == second) return true;
                    if ((first == null) || (second == null)) return false;
                    if (first.Count != second.Count) return false;

                    var comparer = EqualityComparer<TValue>.Default;

                    foreach (KeyValuePair<TKey, TValue> kvp in first)
                    {
                        TValue secondValue;
                        if (!second.TryGetValue(kvp.Key, out secondValue)) return false;
                        if (!comparer.Equals(kvp.Value, secondValue)) return false;
                    }
                    return true;
        }


        static public IDictionary<TKey, TValue> Clone<TKey, TValue>(
            this IDictionary<TKey, TValue> me)
        {
            Dictionary<TKey,TValue>  ret = new Dictionary<TKey,TValue>();

            foreach (KeyValuePair<TKey,TValue> item in me)
            {
                ret.Add(item.Key, item.Value);
            }


            return ret;
        }
      


        /// <summary>
        /// Retorna false se objeto passado for null se a string dentro dele for diferente de 1 ou true
        /// </summary>
        /// <param name="O">Objeto</param>
        /// <returns>bool</returns>
        static public bool ToBool(this object O)
        {
            bool R = false;

            if (O == null)
                R = false;
            else if (O == DBNull.Value)
                R = false;
            else if (O.ToString().Trim() == "")
                R = false;
            else if (O.ToString().ToBool())
                R = true;

            return R;
        }

        /// <summary>
        ///  Retorna false se a string for diferente de 1 ou true
        /// </summary>
        /// <param name="S">String</param>
        /// <returns>bool</returns>
        static public bool ToBool(this string S)
        {
            bool Ret = false;
            if (S.Trim() == "1" || S.Trim().ToLower() == "true")
                Ret = true;

            return Ret;
        }

      

        /// <summary>
        /// Seta valor de um campo da classe
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="PropertyName">Nome do campo</param>
        /// <param name="Value">valor</param>
        static public void SetFieldValue(this object O, string FieldName, object Value)
        {
            O.GetType().GetField(FieldName).SetValue(O, Value);
        }

        /// <summary>
        ///  Retorna o valor de um campo da classe
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="PropertyName">Nome do campo</param>
        /// <param name="Value">valor</param>
        static public object GetFieldValue(this object O, string FieldName)
        {
            return O.GetType().GetField(FieldName).GetValue(O);
        }

        /// <summary>
        /// Return true if the type is a System.Nullable wrapper of a value type
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if the type is a System.Nullable wrapper</returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType
                   && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        static public PropertyInfo GetPropertyInfo(this object O, string PropertyName)
        {
            return O.GetType().GetProperty(PropertyName);
        }

        /// <summary>
        /// Seta valor de uma propriedade da classe
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="PropertyName">Nome da propriedade</param>
        /// <param name="Value">valor</param>
        static public void SetPropertyValue(this object O, string PropertyName, object Value)
        {
            if (PropertyName != string.Empty)
            {
                if (O != null)
                {
                    if (O.GetType().GetProperty(PropertyName) != null)
                    {

                        O.GetType().GetProperty(PropertyName).SetValue(O, Value, null);
                    }
                    else throw new ApplicationException("SetPropertyValue: Propriedade " + PropertyName + " não existe para o objeto.");
                }
                else throw new ApplicationException("SetPropertyValue: Objeto O inválido.");
            }
            else throw new ApplicationException("SetPropertyValue: Nenhuma propriedade enviada.");
        }

        /// <summary>
        ///  Retorna o valor de uma propriedade da classe
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="PropertyName">PropertyIndex</param>
        /// <param name="Value">valor</param>
        static public void SetPropertyValue(this object O, int PropertyIndex, object Value)
        {
            //if (!O.GetType().GetProperties()[PropertyIndex].PropertyType.IsNullable())
            //{
            //    object objChangeType = Convert.ChangeType(Value, O.GetType().GetProperties()[PropertyIndex].PropertyType);
            //    O.GetType().GetProperties()[PropertyIndex].SetValue(O, objChangeType, null);
            //}
            //else
            O.GetType().GetProperties()[PropertyIndex].SetValue(O, Value, null);
        }


        /// <summary>
        /// Retorna o valor de uma propriedade da classe
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="PropertyName">Nome da propriedade</param>
        /// <returns>object</returns>
        static public object GetPropertyValue(this  object O, string Name)
        {
            object objRet = null;
            if (O.GetType().GetProperty(Name) != null)
                objRet = O.GetType().GetProperty(Name).GetValue(O, null);

            return objRet;
        }

        /// <summary>
        /// Retorna o valor de uma propriedade da classe
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="PropertyName">ïndice da propriedade</param>
        /// <returns>object</returns>
        static public object GetPropertyValue(this  object O, int index)
        {
            object objRet = null;
            if (O.GetType().GetProperties()[index] != null)
                objRet = O.GetType().GetProperties()[index].GetValue(O, null);

            return objRet;
        }



        /// <summary>
        /// GetPropertyType
        /// </summary>
        /// <param name="O">this object</param>
        /// <param name="name">Property Name</param>
        /// <returns>Type</returns>
        static public Type GetPropertyType(this object O, string name)
        {
            return O.GetType().GetProperty(name).PropertyType;
        }

        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">nome do método</param>
        /// <param name="Parameters">object parameter</param>
        /// <returns>object</returns>
        static public object InvokeMethod(this object O, string Name)
        {
            return O.GetType().GetMethod(Name, Type.EmptyTypes).Invoke(O, null);
        }

        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">nome do método</param>
        /// <param name="Parameters">object parameter</param>
        /// <returns>object</returns>
        static public object InvokeMethod(this object O, string Name, object Parameter)
        {
            return O.GetType().GetMethod(Name).Invoke(O, new object[] { Parameter });
        }


        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">nome do método</param>
        /// <param name="Parameters">object parameter</param>
        /// <returns>object</returns>
        static public MethodInfo GetMethodInfo(this object O, string Name)
        {
            return O.GetType().GetMethod(Name, Type.EmptyTypes);

        }

        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">nome do método</param>
        /// <param name="Parameters">object parameter</param>
        /// <returns>object</returns>
        static public MethodInfo GetMethodInfo(this object O, string Name, Type[] Types)
        {
            return O.GetType().GetMethod(Name, Types);
        }
        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">nome do método</param>
        /// <param name="Parameters">object parameter</param>
        /// <returns>object</returns>
        static public MethodInfo GetMethodInfo(this object O, string Name, Type T)
        {
            return O.GetType().GetMethod(Name, new Type[] { T });
        }
        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">nome do método</param>
        /// <param name="Parameters">array de parametros</param>
        /// <returns>object</returns>
        static public object InvokeMethod(this object O, string Name, object[] Parameters)
        {
            return O.GetType().GetMethod(Name).Invoke(O, Parameters);
        }


        // gather
        static public void GatherFrom<TSelf, TSource>(this TSelf self, TSource source, bool skipNewValueIfNull, string skipProperties = "")
        {
            var sourceAllProperties = source.GetType().GetProperties();

            var selfType = self.GetType();
            foreach (var sourceProperty in sourceAllProperties)
            {
                var selfProperty = selfType.GetProperty(sourceProperty.Name);

                if (selfProperty == null) continue;

                var sourceValue = sourceProperty.GetValue(source, null);

                if (sourceValue == null && skipNewValueIfNull || skipProperties.ToLower().Contains("@" + sourceProperty.Name.ToLower() + "@"))
                {
                    continue;
                }

                selfProperty.SetValue(self, sourceValue, null);
            }
        }

        static public bool IsEmpty(this object O)
        {
            bool R = false;
            if (O == null)
                R = true;
            else if (O == DBNull.Value)
                R = true;
            else if (string.IsNullOrEmpty( O.ToString().Trim()) )
                R = true;
            else if ( O.GetType()==typeof(int) && (int)O ==0)
                R = true;
            else if (O.GetType() == typeof(double) && (double)O == 0)
                R = true;


            return R;
        }


        static public string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
        }
        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">Nome do método</param>
        /// <param name="Types">array de tipos para definiar a sobrecarga</param>
        /// <param name="Parameters">array de parametros</param>
        /// <returns>object</returns>
        static public object InvokeMethod(this object O, string Name, Type[] Types)
        {
            return O.GetType().GetMethod(Name, Types).Invoke(O, null);
        }
        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">Nome do método</param>
        /// <param name="Types">array de tipos para definiar a sobrecarga</param>
        /// <param name="Parameters">array de parametros</param>
        /// <returns>object</returns>
        static public object InvokeMethod(this object O, string Name, Type[] Types, object[] Parameters)
        {
            return O.GetType().GetMethod(Name, Types).Invoke(O, Parameters);
        }

        /// <summary>
        /// Invoca o metodo
        /// </summary>
        /// <param name="O">object</param>
        /// <param name="Name">Nome do método</param>
        /// <param name="Types">array de tipos para definiar a sobrecarga</param>
        /// <param name="Parameters">array de parametros</param>
        /// <returns>object</returns>
        static public object InvokeMethod(this object O, string Name, Type T, object Parameters)
        {
            return O.GetType().GetMethod(Name, new Type[] { T }).Invoke(O, new object[] { Parameters });
        }

        static public int ToInt32(this string data)
        {
            int result = Int32.Parse(data);
            return result;
        }

       
        static object RowsToDictionary(this System.Data.DataTable table)
        {
            var columns = table.Columns.Cast<System.Data.DataColumn>().ToArray();
            return table.Rows.Cast<System.Data.DataRow>().Select(
                r =>
                    columns.ToDictionary(
                        c => c.ColumnName, c => r[c.ColumnName].ToString()
                    )
            );
        }
        static Dictionary<string, object> ToDictionary(this System.Data.DataTable table)
        {
            return new Dictionary<string, object>
            {
                { table.TableName, table.RowsToDictionary() }
            };
        }
        static Dictionary<string, object> ToDictionary(this System.Data.DataSet data)
        {
            return data.Tables.Cast<System.Data.DataTable>().ToDictionary(t => t.TableName, t => t.RowsToDictionary());
        }
        
        
        /* 
        public static string GetJSONString(System.Data.DataTable table)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(table.ToDictionary());
        }
        public static string GetJSONString(System.Data.DataSet data)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(data.ToDictionary());
        } */


        public static string retiraAcentos(this string texto)
        {
            const string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            const string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        public static string RegexReplace(this string me, string regexp, string replace)
        {
            var re = new System.Text.RegularExpressions.Regex(regexp, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return re.Replace(me, replace);

        }




       public static TResult InvokeEx<TControl, TResult>(this TControl control,
                                           Func<TControl, TResult> func)
       where TControl : Control
        {
            return control.InvokeRequired
                    ? (TResult)control.Invoke(func, control)
                    : func(control);
        }

        public static void InvokeEx<TControl>(this TControl control,
                                              Action<TControl> func)
          where TControl : Control
        {
            control.InvokeEx(c => { func(c); return c; });

        }

        public static void InvokeEx<TControl>(this TControl control, Action action)
          where TControl : Control
        {
            control.InvokeEx(c => action());
        }
        #region "FoxPro"
        /// <summary>
        /// Receives two strings as parameters and searches for one string within another. 
        /// If found, returns the beginning numeric position otherwise returns 0
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.At("D", "Joe Doe");	//returns 5
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        public static int At(this string cSearchIn, string cSearchFor)
        {
            return cSearchIn.IndexOf(cSearchFor) + 1;
        }

        /// <summary>
        /// Receives two strings and an occurence position (1st, 2nd etc) as parameters and 
        /// searches for one string within another for that position. 
        /// If found, returns the beginning numeric position otherwise returns 0
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.At("o", "Joe Doe", 1);	//returns 2
        /// VFPToolkit.strings.At("o", "Joe Doe", 2);	//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        /// <param name="nOccurence"> </param>
        public static int At(this string cSearchIn, string cSearchFor, int nOccurence)
        {
            return __at(cSearchFor, cSearchIn, nOccurence, 1);
        }

        /// Private Implementation: This is the actual implementation of the At() and RAt() functions. 
        /// Receives two strings, the expression in which search is performed and the expression to search for. 
        /// Also receives an occurence position and the mode (1 or 0) that specifies whether it is a search
        /// from Left to Right (for At() function)  or from Right to Left (for RAt() function)
        private static int __at(this string cSearchIn,  string cSearchFor, int nOccurence, int nMode)
        {
            //In this case we actually have to locate the occurence
            int i = 0;
            int nOccured = 0;
            int nPos = 0;
            if (nMode == 1) { nPos = 0; }
            else { nPos = cSearchIn.Length; }

            //Loop through the string and get the position of the requiref occurence
            for (i = 1; i <= nOccurence; i++)
            {
                if (nMode == 1) { nPos = cSearchIn.IndexOf(cSearchFor, nPos); }
                else { nPos = cSearchIn.LastIndexOf(cSearchFor, nPos); }

                if (nPos < 0)
                {
                    //This means that we did not find the item
                    break;
                }
                else
                {
                    //Increment the occured counter based on the current mode we are in
                    nOccured++;

                    //Check if this is the occurence we are looking for
                    if (nOccured == nOccurence)
                    {
                        return nPos + 1;
                    }
                    else
                    {
                        if (nMode == 1) { nPos++; }
                        else { nPos--; }

                    }
                }
            }
            //We never found our guy if we reached here
            return 0;
        }


        /// <summary>
        /// Receives two strings as parameters and searches for one string within another. 
        /// This function ignores the case and if found, returns the beginning numeric position 
        /// otherwise returns 0
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.AtC("d", "Joe Doe");	//returns 5
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        public static int AtC(this string cSearchIn, string cSearchFor)
        {
            return cSearchIn.ToLower().IndexOf(cSearchFor.ToLower()) + 1;
        }

        /// <summary>
        /// Receives two strings and an occurence position (1st, 2nd etc) as parameters and 
        /// searches for one string within another for that position. This function ignores the
        /// case of both the strings and if found, returns the beginning numeric position 
        /// otherwise returns 0.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.AtC("d", "Joe Doe", 1);	//returns 5
        /// VFPToolkit.strings.AtC("O", "Joe Doe", 2);	//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        /// <param name="nOccurence"> </param>
        public static int AtC(this string cSearchIn, string cSearchFor, int nOccurence)
        {
            return __at(cSearchFor.ToLower(), cSearchIn.ToLower(), nOccurence, 1);
        }

        /// <summary>
        /// Receives an expression and a list of values as parameter and returns a true
        /// if the expression exists in the list. Please note that the Visual FoxPro's
        /// InList() function has a limitation of 23 items whereas this one does not have
        /// a limitation on the number of items passed.
        /// </summary>
        /// <example>
        /// Console.WriteLine(InList("Kamal", "Pat", "abc", "Kamal"));	//returns true
        /// Console.WriteLine(InList("Kamal", "Pat", "abc", "xyz"));	//returns false
        /// Console.WriteLine(InList(123, 12, 13, 16, 1717, 123));	//returns true
        /// </example>
        /// <param name="tcExpression"></param>
        /// <param name="toVar"></param>
        /// <returns></returns>
        public static bool InList(this object toExpression, params object[] toItems)
        {
            return Array.IndexOf(toItems, toExpression) > -1;
        }

        /// <summary>
        /// Receives an expression, low and high values and return a bool specifying 
        /// if the value was between the low and high values. This contains overloads 
        /// for int, float, decimal, char, date and string data types.
        /// <p/><pre>
        /// Example:
        /// if(VFPToolkit.common.Between(5,7,29))....
        /// </pre>
        /// </summary>
        /// <param name="tnExpression"></param>
        /// <param name="tnLowValue"></param>
        /// <param name="tnHighValue"></param>
        /// <returns></returns>
        public static bool Between(this int tnExpression, int tnLowValue, int tnHighValue)
        {
            return ((tnExpression >= tnLowValue) && (tnExpression <= tnHighValue));
        }
        public static bool Between(this double tnExpression, double tnLowValue, double tnHighValue)
        {
            return ((tnExpression >= tnLowValue) && (tnExpression <= tnHighValue));
        }
        public static bool Between(this decimal tnExpression, decimal tnLowValue, decimal tnHighValue)
        {
            return ((tnExpression >= tnLowValue) && (tnExpression <= tnHighValue));
        }

        // Compares between for date range
        public static bool Between(this System.DateTime tdDateTime, System.DateTime tdStartDate, System.DateTime tdEndDate)
        {
            return ((tdDateTime >= tdStartDate) && (tdDateTime <= tdEndDate));
        }

        // Compares between for char
        public static bool Between(this char tcChar, char tcLowChar, char tcHighChar)
        {
            return (((int)tcChar >= (int)tcLowChar) && ((int)tcChar <= (int)tcHighChar));
        }

        // Compares between for strings
        // The way strings are compared in VFP is interesting
        public static bool Between(this string tcExpression, string tcStart, string tcEnd)
        {
            bool llRetVal = true;

            // We start with the start string, tcStart, and compare each character in this
            // with tcExpression. If we fail at anytime we return a false
            for (int i = 0; i < tcStart.Length; i++)
            {
                if (tcStart[i] < tcExpression[i])
                {
                    llRetVal = false;
                    break;
                }

                //if we have reached the end of tcExpression break
                if (i == tcExpression.Length)
                    break;
            }


            // The way strings are compared in VFP is interesting
            // We start with the start string, tcStart, and compare each character in this
            // with tcExpression. If we fail at anytime we return a false
            for (int i = 0; i < tcEnd.Length; i++)
            {
                if (tcEnd[i] > tcExpression[i])
                {
                    llRetVal = false;
                    break;
                }

                //if we have reached the end of tcExpression break
                if (i == tcExpression.Length)
                    break;
            }

            return llRetVal;

        }


        /// <summary>
        /// Receives two objects as parameters and returns the one which is not null. If both of them are null
        /// then returns a null, if the first one is not null then returns the first object.
        /// <pre>
        /// Example:
        /// string myNullObj;	//The string is not initialized yet
        /// VFPToolkit.common.NVL("mystring", myNullObj);	//returns mystring object
        /// Note: All strings, int, long etc. all of them are objects
        /// </summary>
        /// <param name="oExp1"></param>
        /// <param name="oExp2"></param>
        /// <returns></returns>
        public static object Nvl(this object oExp1, object oExp2)
        {
            //if oExp1 is not null then return oExp1
            if (oExp1 != null)
            {
                return oExp1;
            }
            else if ((oExp1 == null) && (oExp2 != null))
            {
                //If oExp1 is null return oExp2
                return oExp2;
            }
            else
                //If both of them are null return nothing
                return null;
        }

        /// <summary>
        /// Receives an object (string, int etc.) as a parameter and returns the datatype of that object.
        /// Unlike VFP, this method returns the .NET type. So, for a string object it
        /// will return "System.String" and not "C". This will allow you to get the type
        /// of any object including custom classes you develop. For a string object, 
        /// this method will return "System.String" instead of a "C" in VFP.
        /// <pre>
        /// Example:
        /// VFPToolkit.common.VarType(MyObject);	//returns the type of object
        /// </pre>
        /// </summary>
        /// <param name="oObj"></param>
        /// <returns></returns>
        public static string VarType(this object oObj)
        {
            //Return a string that specifies the type of the object
            if (oObj == null)
                return "null";
            else
                return oObj.GetType().ToString();
        }

        /// <summary>
        /// Searches one string into another string and replaces all occurences with
        /// a blank character.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.StrTran("Joe Doe", "o");		//returns "J e D e" :)
        /// </pre>
        /// </summary>
        /// <param name="cSearchIn"> </param>
        /// <param name="cSearchFor"> </param>
        public static string StrTran(this string cSearchIn, string cSearchFor)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //Call the Replace() method of the StringBuilder
            return sb.Replace(cSearchFor, " ").ToString();
        }
        /// <summary>
        /// Searches one string into another string and replaces all occurences with
        /// a third string.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.StrTran("Joe Doe", "o", "ak");		//returns "Jake Dake" 
        /// </pre>
        /// </summary>
        /// <param name="cSearchIn"> </param>
        /// <param name="cSearchFor"> </param>
        /// <param name="cReplaceWith"> </param>
        public static string StrTran(this string cSearchIn, string cSearchFor, string cReplaceWith)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //There is a bug in the replace method of the StringBuilder
            sb.Replace(cSearchFor, cReplaceWith);

            //Call the Replace() method of the StringBuilder and specify the string to replace with
            return sb.Replace(cSearchFor, cReplaceWith).ToString();
        }

        /// Searches one string into another string and replaces each occurences with
        /// a third string. The fourth parameter specifies the starting occurence and the 
        /// number of times it should be replaced
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.StrTran("Joe Doe", "o", "ak", 2, 1);		//returns "Joe Dake" 
        /// </pre>
        public static string StrTran(this string cSearchIn, string cSearchFor, string cReplaceWith, int nStartoccurence, int nCount)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //There is a bug in the replace method of the StringBuilder
            sb.Replace(cSearchFor, cReplaceWith);

            //Call the Replace() method of the StringBuilder specifying the replace with string, occurence and count
            return sb.Replace(cSearchFor, cReplaceWith, nStartoccurence, nCount).ToString();
        }


        /// <summary>
        /// Replaces each character in a character expression that matches a character
        /// in a second character expression with the corresponding character in a 
        /// third character expression
        /// </summary>
        /// <example>
        /// Console.WriteLine(ChrTran("ABCDEF", "ACE", "XYZ"));  //Displays XBYDZF
        /// Console.WriteLine(ChrTran("ABCD", "ABC", "YZ"));	//Displays YZD
        /// Console.WriteLine(ChrTran("ABCDEF", "ACE", "XYZQRST"));	//Displays XBYDZF
        /// </example>
        /// <param name="cSearchIn"> </param>
        /// <param name="cSearchFor"> </param>
        /// <param name="cReplaceWith"> </param>
        public static string ChrTran(this string cSearchIn, string cSearchFor, string cReplaceWith)
        {
            string lcRetVal = cSearchIn;
            string cReplaceChar;
            for (int i = 0; i < cSearchFor.Length; i++)
            {
                if (cReplaceWith.Length <= i)
                    cReplaceChar = "";
                else
                    cReplaceChar = cReplaceWith[i].ToString();

                lcRetVal = StrTran(lcRetVal, cSearchFor[i].ToString(), cReplaceChar);
            }
            return lcRetVal;
        }


        /// <summary>
        /// Receives a file name as a parameter and returns the contents of that file
        /// as a string.
        /// </summary>
        /// Example:
        /// VFPToolkit.strings.FileToStr("c:\\My Folders\\MyFile.txt");	//returns the contents of the file
        /// </pre>
        /// </summary>
        /// <param name="cFileName"> </param>
        public static string FileToStr(this string me, string cFileName)
        {
            //Create a StreamReader and open the file
            StreamReader oReader = System.IO.File.OpenText(cFileName);

            //Read all the contents of the file in a string
            string lcString = oReader.ReadToEnd();

            //Close the StreamReader and return the string
            oReader.Close();
            me = lcString;
            return lcString;
        }

        /// <summary>
        /// Receives a string and the number of characters as parameters and returns the
        /// specified number of leftmost characters of that string
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Left("Joe Doe", 3);	//returns "Joe"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nDigits"> </param>
        public static string Left(this string cExpression, int nDigits)
        {
            return cExpression.Substring(0, nDigits);
        }

        /// <summary>
        /// Returns the number of occurences of a character within a string
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Occurs('o', "Joe Doe");		//returns 2
        /// 
        /// Tip: If we have a string say lcString, then lcString[3] gives us the 3rd character in the string
        /// </pre>
        /// </summary>
        /// <param name="cChar"> </param>
        /// <param name="cExpression"> </param>
        public static int Occurs(this char tcChar, string cExpression)
        {
            int i, nOccured = 0;

            //Loop through the string
            for (i = 0; i < cExpression.Length; i++)
            {
                //Check if each expression is equal to the one we want to check against
                if (cExpression[i] == tcChar)
                {
                    //if  so increment the counter
                    nOccured++;
                }
            }
            return nOccured;
        }

        /// <summary>
        /// Returns the number of occurences of a character within a string
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Occurs('o', "Joe Doe");		//returns 2
        /// 
        /// Tip: If we have a string say lcString, then lcString[3] gives us the 3rd character in the string
        /// </pre>
        /// </summary>
        /// <param name="cChar"> </param>
        /// <param name="cExpression"> </param>
        public static int Occurs(this string cString, string cExpression)
        {
            int nPos = 0;
            int nOccured = 0;
            do
            {
                //Look for the search string in the expression
                nPos = cExpression.IndexOf(cString, nPos);

                if (nPos < 0)
                {
                    //This means that we did not find the item
                    break;
                }
                else
                {
                    //Increment the occured counter based on the current mode we are in
                    nOccured++;
                    nPos++;
                }
            } while (true);

            //Return the number of occurences
            return nOccured;
        }

        /// <summary>
        /// Receives a string and the length of the result string as parameters. Pads blank 
        /// characters on the both sides of this string and returns a string with the length specified.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.PadL("Joe Doe", 10);		//returns " Joe Doe  "
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        public static string PadC(this string cExpression, int nResultSize)
        {
            //Determine the number of padding characters
            int nPaddTotal = nResultSize - cExpression.Length;
            int lnHalfLength = (int)(nPaddTotal / 2);

            string lcString = PadL(cExpression, cExpression.Length + lnHalfLength);
            return lcString.PadRight(nResultSize);
        }

        /// <summary>
        /// Receives a string, the length of the result string and the padding character as 
        /// parameters. Pads the padding character on both sides of this string and returns a string 
        /// with the length specified.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.PadL("Joe Doe", 10, 'x');		//returns "xJoe Doexx"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        /// <param name="cPaddingChar"> </param>
        public static string PadC(this string cExpression, int nResultSize, char cPaddingChar)
        {
            //Determine the number of padding characters
            int nPaddTotal = nResultSize - cExpression.Length;
            int lnHalfLength = (int)(nPaddTotal / 2);

            string lcString = PadL(cExpression, cExpression.Length + lnHalfLength, cPaddingChar);
            return lcString.PadRight(nResultSize, cPaddingChar);
        }

        /// <summary>
        /// Receives a string and the length of the result string as parameters. Pads blank 
        /// characters on the left of this string and returns a string with the length specified.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.PadL("Joe Doe", 10);		//returns "   Joe Doe"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        public static string PadL(this string cExpression, int nResultSize)
        { return cExpression.PadLeft(nResultSize); }

        /// <summary>
        /// Receives a string, the length of the result string and the padding character as 
        /// parameters. Pads the padding character on the left of this string and returns a string 
        /// with the length specified.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.PadL("Joe Doe", 10, 'x');		//returns "xxxJoe Doe"
        /// 
        /// Tip: Use single quote to create a character type data and double quotes for strings
        /// </pre>
        /// </summary>
        public static string PadL(this string cExpression, int nResultSize, char cPaddingChar)
        { return cExpression.PadLeft(nResultSize, cPaddingChar); }

        /// <summary>
        /// Receives a string and the length of the result string as parameters. Pads blank 
        /// characters on the right of this string and returns a string with the length specified.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.PadL("Joe Doe", 10);		//returns "Joe Doe   "
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        public static string PadR(this string cExpression, int nResultSize)
        { return cExpression.PadRight(nResultSize); }


        /// <summary>
        /// Receives a string, the length of the result string and the padding character as 
        /// parameters. Pads the padding character on the right of this string and returns a string 
        /// with the length specified.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.PadL("Joe Doe", 10, 'x');		//returns "Joe Doexxx"
        /// 
        /// Tip: Use single quote to create a character type data and double quotes for strings
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        /// <param name="cPaddingChar"> </param>
        public static string PadR(this string cExpression, int nResultSize, char cPaddingChar)
        { return cExpression.PadRight(nResultSize, cPaddingChar); }

        /// <summary>
        /// Receives a string as a parameter and returns the string in Proper format (makes each letter after a space capital)
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Proper("joe doe is a good man");	//returns "Joe Doe Is A Good Man"
        /// </pre>
        /// </summary>
        /// <param name="cString"> </param>
        /// ToDo: Split the string instead and you do not have to worry about comparing each char
        public static string Proper(this string cString)
        {

            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cString);

            int i, j = 0;
            int nLength = cString.Length;

            for (i = 0; i < nLength; i++)
            {
                //look for a blank space and once found make the next character to uppercase
                if ((i == 0) || (char.IsWhiteSpace(cString[i])))
                {
                    //Handle the first character differently
                    if (i == 0) { j = i; }
                    else { j = i + 1; }

                    //Make the next character uppercase and update the stringBuilder
                    sb.Remove(j, 1);
                    sb.Insert(j, Char.ToUpper(cString[j]));
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// Receives two strings as parameters and searches for one string within another. 
        /// The search is performed starting from Right to Left and if found, returns the 
        /// beginning numeric position otherwise returns 0
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.RAt("o", "Joe Doe");	//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        public static int RAt(this string cSearchIn, string cSearchFor)
        {
            return cSearchIn.LastIndexOf(cSearchFor) + 1;
        }

        /// <summary>
        /// Receives two strings as parameters and an occurence position as parameters. 
        /// The function searches for one string within another and the search is performed 
        /// starting from Right to Left and if found, returns the beginning numeric position 
        /// otherwise returns 0
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.RAt("o", "Joe Doe", 1);	//returns 6
        /// VFPToolkit.strings.RAt("o", "Joe Doe", 2);	//returns 2
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        /// <param name="nOccurence"> </param>
        public static int RAt(this string cSearchIn, string cSearchFor, int nOccurence)
        {
            return __at(cSearchFor, cSearchIn, nOccurence, 0);
        }

        /// <summary>
        /// Receives a string expression and a numeric value indicating number of time
        /// and replicates that string for the specified number of times.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Replicate("Joe", 5);		//returns JoeJoeJoeJoeJoe
        /// 
        /// Tip: Use a StringBuilder when lengthy string manipulations are required.
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nTimes"> </param>
        public static string Replicate(this string cExpression, int nTimes)
        {
            //Create a stringBuilder
            StringBuilder sb = new StringBuilder();

            //Insert the expression into the StringBuilder for nTimes
            sb.Insert(0, cExpression, nTimes);

            //Convert it to a string and return it back
            return sb.ToString();
        }

        /// <summary>
        /// Receives a string and the number of characters as parameters and returns the
        /// specified number of rightmost characters of that string
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Right("Joe Doe", 3);	//returns "Doe"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nDigits"> </param>
        public static string Right(this string cExpression, int nDigits)
        {
            return cExpression.Substring(cExpression.Length - nDigits);
        }

        /// <summary>
        /// Receives a string along with starting and ending delimiters and returns the 
        /// part of the string between the delimiters. Receives a beginning occurence
        /// to begin the extraction from and also receives a flag (0/1) where 1 indicates
        /// that the search should be case insensitive.
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// VFPToolkit.strings.StrExtract(cExpression, "o", "eJ", 1, 0);		//returns "eDo"
        /// </pre>
        /// </summary>
        public static string StrExtract(this string cSearchExpression, string cBeginDelim, string cEndDelim, int nBeginOccurence, int nFlags)
        {
            string cstring = cSearchExpression;
            string cb = cBeginDelim;
            string ce = cEndDelim;
            string lcRetVal = "";

            //Check for case-sensitive or insensitive search
            if (nFlags == 1)
            {
                cstring = cstring.ToLower();
                cb = cb.ToLower();
                ce = ce.ToLower();
            }

            //Lookup the position in the string
            int nbpos = At(cb, cstring, nBeginOccurence) + cb.Length - 1;
            int nepos = cstring.IndexOf(ce, nbpos + 1);

            //Extract the part of the strign if we get it right
            if (nepos > nbpos)
            {
                lcRetVal = cSearchExpression.Substring(nbpos, nepos - nbpos);
            }

            return lcRetVal;
        }

        /// <summary>
        /// Receives a string and a delimiter as parameters and returns a string starting 
        /// from the position after the delimiter
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// VFPToolkit.strings.StrExtract(cExpression, "o");		//returns "eDoeJoeDoe"
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression"> </param>
        /// <param name="cBeginDelim"> </param>
        public static string StrExtract(this string cSearchExpression, string cBeginDelim)
        {
            int nbpos = At(cBeginDelim, cSearchExpression);
            return cSearchExpression.Substring(nbpos + cBeginDelim.Length - 1);
        }

        /// <summary>
        /// Receives a string along with starting and ending delimiters and returns the 
        /// part of the string between the delimiters
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// VFPToolkit.strings.StrExtract(cExpression, "o", "eJ");		//returns "eDo"
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression"> </param>
        /// <param name="cBeginDelim"> </param>
        /// <param name="cEndDelim"> </param>
        public static string StrExtract(this string cSearchExpression, string cBeginDelim, string cEndDelim)
        {
            return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, 1, 0);
        }

        /// <summary>
        /// Receives a string along with starting and ending delimiters and returns the 
        /// part of the string between the delimiters. It also receives a beginning occurence
        /// to begin the extraction from.
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// VFPToolkit.strings.StrExtract(cExpression, "o", "eJ", 2);		//returns ""
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression"> </param>
        /// <param name="cBeginDelim"> </param>
        /// <param name="cEndDelim"> </param>
        /// <param name="nBeginOccurence"> </param>
        public static string StrExtract(this string cSearchExpression, string cBeginDelim, string cEndDelim, int nBeginOccurence)
        {
            return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, nBeginOccurence, 0);
        }

        /// <summary>
        /// Receives a string and a file name as parameters and writes the contents of the
        /// string to that file
        /// <pre>
        /// Example:
        /// string lcString = "This is the line we want to insert in our file.";
        /// VFPToolkit.strings.StrToFile(lcString, "c:\\My Folders\\MyFile.txt");
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="cFileName"> </param>
        public static void StrToFile(this string cExpression, string cFileName)
        {
            //Check if the sepcified file exists
            if (System.IO.File.Exists(cFileName) == true)
            {
                //If so then Erase the file first as in this case we are overwriting
                System.IO.File.Delete(cFileName);
            }

            //Create the file if it does not exist and open it
            FileStream oFs = new FileStream(cFileName, FileMode.CreateNew, FileAccess.ReadWrite);

            //Create a writer for the file
            StreamWriter oWriter = new StreamWriter(oFs);

            //Write the contents
            oWriter.Write(cExpression);
            oWriter.Flush();
            oWriter.Close();

            oFs.Close();
        }

        /// <summary>
        /// Receives a string and a file name as parameters and writes the contents of the
        /// string to that file. Receives an additional parameter specifying whether the 
        /// contents should be appended at the end of the file
        /// <pre>
        /// Example:
        /// string lcString = "This is the line we want to insert in our file.";
        /// VFPToolkit.strings.StrToFile(lcString, "c:\\My Folders\\MyFile.txt");
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="cFileName"> </param>
        /// <param name="lAdditive"> </param>
        public static void StrToFile(this string cExpression, string cFileName, bool lAdditive)
        {
            //Create the file if it does not exist and open it
            FileStream oFs = new FileStream(cFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //Create a writer for the file
            StreamWriter oWriter = new StreamWriter(oFs);

            //Set the pointer to the end of file
            oWriter.BaseStream.Seek(0, SeekOrigin.End);

            //Write the contents
            oWriter.Write(cExpression);
            oWriter.Flush();
            oWriter.Close();
            oFs.Close();
        }

        /// <summary>
        /// Receives a string (cExpression) as a parameter and replaces a specified number 
        /// of characters in that string (nCharactersReplaced) from a specified location
        /// (nStartReplacement) with a specified string (cReplacement)
        /// <pre>
        /// Example:
        /// string lcString = "Joe Doe";
        /// string lcReplace = "Foo ";
        /// VFPToolkit.strings.Stuff(lcString, 5, 0, lcReplace);	//returns "Joe Foo Doe";
        /// VFPToolkit.strings.Stuff(lcString, 5, 3, lcReplace);	//returns "Joe Foo ";
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nStartReplacement"> </param>
        /// <param name="nCharactersReplaced"> </param>
        /// <param name="cReplacement"> </param>
        public static string Stuff(this string cExpression, int nStartReplacement, int nCharactersReplaced, string cReplacement)
        {
            //Create a stringbuilder to work with the string
            StringBuilder sb = new StringBuilder(cExpression);

            if (nCharactersReplaced + nStartReplacement - 1 >= cExpression.Length)
                nCharactersReplaced = cExpression.Length - nStartReplacement + 1;


            //First remove the characters specified in nCharacterReplaced
            if (nCharactersReplaced != 0)
            {
                sb.Remove(nStartReplacement - 1, nCharactersReplaced);
            }

            //Now Add the new string at the right location
            //sb.Insert(0,cExpression,nTimes);
            sb.Insert(nStartReplacement - 1, cReplacement);
            return sb.ToString();
        }


        /// <summary>
        /// Receives a string and converts it to an integer
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.Val("1325");	//returns 1325
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        public static int Val(this string cExpression)
        {
            //Remove all the spaces and commas from the string
            //Get the integer portion of the string
            Int32 ret = 0;
           if (! Int32.TryParse(cExpression,out ret))
               ret=0;

           return ret;
        }



        /// <summary>
        /// Receives a string and converts it to an integer
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.AtLine("Is", "Is Life Beautiful? \r\n It sure is");	//returns 1
        /// </pre>
        /// </summary>
        /// <param name="tcSearchExpression"></param>
        /// <param name="tcExpressionSearched"></param>
        /// <returns></returns>
        public static int AtLine(this string tcSearchExpression, string tcExpressionSearched)
        {
            string lcString;
            int nPosition;
            int nCount = 0;

            try
            {
                nPosition = At(tcSearchExpression, tcExpressionSearched);
                if (nPosition > 0)
                {
                    lcString = tcExpressionSearched.Substring(0, nPosition);
                    nCount = Occurs(@"\r", lcString) + 1;
                }
            }
            catch
            {
                nCount = 0;
            }

            return nCount;
        }

        /// <summary>
        /// Receives a search expression and string to search as parameters and returns an integer specifying
        /// the line where it was found. This function starts it search from the end of the string.
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.RAtLine("sure", "Is Life Beautiful? \r\n It sure is") 'returns 2
        /// </pre>
        /// </summary>
        /// <param name="tcSearchExpression"></param>
        /// <param name="tcExpressionSearched"></param>
        /// <returns></returns>
        public static int RAtLine(this string tcSearchExpression, string tcExpressionSearched)
        {
            string lcString;
            int nPosition;
            int nCount = 0;

            try
            {
                nPosition = RAt(tcSearchExpression, tcExpressionSearched);
                if (nPosition > 0)
                {
                    lcString = tcExpressionSearched.Substring(0, nPosition );
                    nCount = Occurs(@"\r", lcString) + 1;
                }
            }
            catch
            {
                nCount = 0;
            }

            return nCount;
        }

        /// <summary>
        /// Returns the line number of the first occurence of a string expression within 
        /// another string expression without regard to case (upper or lower)
        /// <pre>
        /// Example:
        /// VFPToolkit.strings.AtCLine("Is Life Beautiful? \r\n It sure is", "Is");	//returns 1
        /// </pre>
        /// </summary>
        /// <param name="tcSearchExpression"></param>
        /// <param name="tcExpressionSearched"></param>
        /// <returns></returns>
        public static int AtCLine(this string tcSearchExpression, string tcExpressionSearched)
        {
            return AtLine(tcSearchExpression.ToLower(), tcExpressionSearched.ToLower());
        }

        /// <summary>
        /// Returns the number of lines in a string
        /// <pre>
        /// Example:
        /// int lnLines = VFPToolkit.strings.MemLines(lcMyLongString);
        /// </pre>
        /// </summary>
        /// <param name="tcString"></param>
        /// <returns></returns>
        public static int MemLines(this string tcString)
        {
            if (tcString.Trim().Length == 0)
                return 0;
            else
                return Occurs("\\r", tcString) + 1;
        }

        /// <summary>
        /// Receives a string and a line number as parameters and returns the
        /// specified line in that string
        /// <pre>
        /// Example:
        /// string lcCity = VFPToolkit.strings.MLine(tcAddress, 2); // Not that you would want to do something like this but you could ;)
        /// </pre>
        /// </summary>
        /// <param name="tcString"></param>
        /// <param name="tnLineNo"></param>
        /// <returns></returns>
        public static string MLine(this string tcString, int tnLineNo)
        {
            string[] aLines = tcString.Split('\r');
            string lcRetVal = "";
            try
            {
                lcRetVal = aLines[tnLineNo - 1];
            }
            catch
            {
                //Ignore the exception as MLINE always returns a value
            }

            return lcRetVal;
        }



        #endregion

    }
}