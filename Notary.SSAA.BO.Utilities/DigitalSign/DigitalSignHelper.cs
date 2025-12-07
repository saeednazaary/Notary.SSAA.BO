using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Notary.SSAA.BO.Utilities.DigitalSign
{
    public class DataGraphSignHelper
    {
        public static SignData GetDataSignText(object obj, SignInfoDataGraphNode signInfoDataGraph)
        {
            return GetDataSignText(obj, signInfoDataGraph, true);
        }
        public static SignData GetDataSignText(object obj, SignInfoDataGraphNode signInfoDataGraph, bool useEntityFormatting)
        {
            SignData signData = new();
            StringBuilder sb = new();
            Type objType = obj.GetType();
            object newObj = Activator.CreateInstance(objType);
            signData.Data = newObj;
            if (signInfoDataGraph.SignElementsFields != null)
            {
                bool isFirstField = true;
                bool hasAnyNonHullValue = false;
                foreach (string fieldName in signInfoDataGraph.SignElementsFields)
                {
                    PropertyInfo propInfo = objType.GetProperty(fieldName);
                    if (propInfo != null)
                    {
                        object fieldValue = propInfo.GetValue(obj);
                        if (fieldValue != null)
                        {
                            propInfo.SetValue(newObj, fieldValue, null);
                            hasAnyNonHullValue = true;
                            if (!isFirstField)
                            {
                                if (useEntityFormatting)
                                {
                                    _ = sb.Append(',');
                                }
                            }
                            else
                            {
                                if (useEntityFormatting)
                                {
                                    _ = sb.Append('{');
                                }
                                isFirstField = false;
                            }
                            if (useEntityFormatting)
                            {
                                _ = sb.Append(fieldName);
                                _ = sb.Append(':');
                                _ = sb.Append(string.Format("'{0}'", GetFieldStringValue(fieldValue)));
                            }
                            else
                            {
                                _ = sb.Append(GetFieldStringValue(fieldValue));
                            }
                        }
                    }
                }
                if (hasAnyNonHullValue && useEntityFormatting)
                {
                    _ = sb.Append('}');
                }
            }
            if (signInfoDataGraph.RelationsDataGraphs != null)
            {
                foreach (SignInfoDataGraphNode relationSignInfoDataGraph in signInfoDataGraph.RelationsDataGraphs)
                {
                    PropertyInfo propInfo = objType.GetProperty(relationSignInfoDataGraph.RelationName);
                    if (propInfo != null)
                    {
                        object fieldValue = propInfo.GetValue(obj);
                        if (fieldValue != null)
                        {
                            if (fieldValue is IEnumerable)
                            {
                                IEnumerable sortedList = (IEnumerable)fieldValue;//GetSortedEntityList((IEnumerable)fieldValue, false);
                                foreach (object element in sortedList)
                                {
                                    SignData sd = GetDataSignText(element, relationSignInfoDataGraph);
                                    _ = sb.Append(sd.StringData);
                                    _ = propInfo.PropertyType.GetMethod("Add").Invoke(propInfo.GetValue(newObj), new object[] { sd.Data });
                                }

                            }
                            else
                            {
                                SignData sd = GetDataSignText(fieldValue, relationSignInfoDataGraph);
                                _ = sb.Append(sd.StringData);
                                propInfo.SetValue(newObj, sd.Data);
                            }
                        }
                    }

                }
            }
            signData.StringData = sb.ToString();
            return signData;
        }
        private static string GetFieldStringValue(object fieldValue)
        {
            return fieldValue != null
                ? fieldValue is double || fieldValue is double? ? GetDoubleFieldValueText(fieldValue) : fieldValue.ToString()
                : null;
        }
        private static string GetDoubleFieldValueText(object fieldValue)
        {
            if (fieldValue != null)
            {
                if (fieldValue is double)
                {
                    return GetDoubleValueText((double)fieldValue);
                }
                else if (fieldValue is double?)
                {
                    return GetDoubleValueText((double?)fieldValue);
                }
            }
            return null;
        }
        private static string GetDoubleValueText(double? value)
        {
            string strValue = "";
            if (value.HasValue)
            {
                strValue = value.ToString();
                string decimalSeperator = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                strValue = strValue.Replace(decimalSeperator, ".");
            }
            return strValue;
        }
        private static string GetDoubleValueText(decimal? value)
        {
            return value.HasValue ? GetDoubleValueText(value.Value) : null;
        }

        private static IEnumerable GetSortedEntityList(IEnumerable entityList, bool fDescending)
        {
            if (!IsNullOrEmptyList(entityList))
            {
                Type elementType = GetListElementsType(entityList);

                if (elementType == null)
                {
                    throw new InvalidOperationException("Could not determine element type of the list");
                }

                Type genericListType = typeof(List<>).MakeGenericType(elementType);
                PropertyInfo property = elementType.GetProperty("Id");

                if (property == null)
                {
                    throw new InvalidOperationException($"Element type {elementType.Name} does not have an 'Id' property");
                }

                IList listToBeSorted = (IList)Activator.CreateInstance(genericListType);

                foreach (object element in entityList)
                {
                    listToBeSorted.Add(element);
                }

                SortList(listToBeSorted, property, !fDescending);
                return listToBeSorted;
            }
            else
            {
                return entityList;
            }
        }
        public static void SortList(object dataSource, PropertyInfo property, bool isAscending)
        {
            List<object> list = [];
            IList tmpLst = (IList)dataSource;
            foreach (object item in tmpLst)
            {
                list.Add(item);
            }
            Comparison<object> compare = delegate (object a, object b)
            {
                bool asc = isAscending;
                object valueA = asc ? property.GetValue(a, null) : property.GetValue(b, null);
                object valueB = asc ? property.GetValue(b, null) : property.GetValue(a, null);

                return valueA is IComparable ? ((IComparable)valueA).CompareTo(valueB) : 0;
            };
            list.Sort(compare);
        }
        private static bool IsNullOrEmptyList(IEnumerable list)
        {
            if (list == null)
            {
                return true;
            }

            foreach (object element in list)
            {
                return false;
            }
            return true;
        }
        private static Type GetListElementsType(IEnumerable list)
        {
            if (list == null)
            {
                return null;
            }

            foreach (object element in list)
            {
                return element.GetType();
            }
            return null;


        }


    }

    public class SignInfoDataGraphNode
    {
        public SignInfoDataGraphNode()
        {
            this._relationName = null;
            _nodes = [];

        }

        public SignInfoDataGraphNode(string relationName)
        {
            this._relationName = relationName;
            _nodes = [];

        }
        private string[] _signElementsFields;
        public string[] SignElementsFields
        {
            get => _signElementsFields; set => _signElementsFields = value;
        }
        private IList<SignInfoDataGraphNode> _nodes;
        public IList<SignInfoDataGraphNode> RelationsDataGraphs
        {
            get => _nodes; set => _nodes = value;
        }
        private string _relationName;
        public string RelationName
        {
            get => _relationName; set => _relationName = value;
        }
        private SignInfoDataGraphNode FindNodeByRelationName(string relationName)
        {
            foreach (SignInfoDataGraphNode node in this._nodes)
            {
                if (node.RelationName == relationName)
                {
                    return node;
                }
            }
            return null;
        }
        private SignInfoDataGraphNode AddNode(string relationName)
        {
            SignInfoDataGraphNode node = new(relationName);
            _nodes.Add(node);
            return node;
        }
        public void AddNode(SignInfoDataGraphNode node)
        {
            _nodes.Add(node);
        }

        public SignInfoDataGraphNode Node(string relationName)
        {
            SignInfoDataGraphNode node = this.FindNodeByRelationName(relationName);
            if (node == null)
            {
                node = AddNode(relationName);
            }

            return node;
        }

        public SignInfoDataGraphNode GetPathNode(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                string[] pathParts = path.Split('.');
                SignInfoDataGraphNode currentNode = this;
                foreach (string nodeRelationName in pathParts)
                {
                    currentNode = currentNode.Node(nodeRelationName);
                }
                return currentNode;
            }
            else
            {
                return null;
            }
        }

    }

    public class SignInfoDataGraph : SignInfoDataGraphNode
    {
        public SignInfoDataGraph() : base(null)
        {
        }

    }
    public class SignGraphNode
    {
        public string Name
        {
            get;
            set;
        }
        public string[] Fields
        {
            get;
            set;
        }
        public SignGraphNode[] SubNodes
        {
            get;
            set;
        }
    }
    public class FormSignInfoDataGraphProvider
    {
        private SignInfoDataGraph GetSignInfoDataGraph(SignGraphNode signGraphNode)
        {
            SignInfoDataGraph signInfoDataGraph = new();
            FillSignInfoDataGraphNode(signInfoDataGraph, signGraphNode);
            return signInfoDataGraph;
        }

        private SignInfoDataGraphNode GetSignInfoDataGraphNode(SignGraphNode signGraphNode)
        {
            SignInfoDataGraphNode signInfoDataGraphNode = new(signGraphNode.Name);
            FillSignInfoDataGraphNode(signInfoDataGraphNode, signGraphNode);
            return signInfoDataGraphNode;
        }

        private void FillSignInfoDataGraphNode(SignInfoDataGraphNode signInfoDataGraphNode, SignGraphNode signGraphNode)
        {
            signInfoDataGraphNode.SignElementsFields = signGraphNode.Fields;
            foreach (SignGraphNode node in signGraphNode.SubNodes)
            {
                signInfoDataGraphNode.AddNode(GetSignInfoDataGraphNode(node));

            }
        }

        public SignInfoDataGraph GetFormSignInfoDataGraph(string descriptor)
        {
            SignGraphNode signatureDefinitionObject = JsonSerializer.Deserialize<SignGraphNode>(descriptor);
            return signatureDefinitionObject.SubNodes.Length > 0 ? GetSignInfoDataGraph(signatureDefinitionObject.SubNodes[0]) : null;
        }
    }

    public class SignData
    {
        public object Data { get; set; }
        public string StringData { get; set; }
    }
}
