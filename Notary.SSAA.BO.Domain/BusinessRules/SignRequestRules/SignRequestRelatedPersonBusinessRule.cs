using Notary.SSAA.BO.Domain.Entities;
using System.Text;

namespace Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules
{
    public static class SignRequestRelatedPersonBusinessRule
    {
        public static bool CheckLoopInPersonRelated(ICollection<SignRequestPersonRelated> signRequestPersonRelated)
        {
            if (signRequestPersonRelated == null || signRequestPersonRelated.Count == 0)
                return true;

            // Build adjacency list: person -> list of people they represent
            var graph = new Dictionary<Guid, List<Guid>>();

            foreach (var relation in signRequestPersonRelated)
            {
                if (relation.MainPersonId == null || relation.AgentPersonId == null)
                    continue;

                // Direct loop check
                if (relation.MainPersonId == relation.AgentPersonId)
                    return false;

                if (!graph.ContainsKey(relation.MainPersonId))
                    graph[relation.MainPersonId] = new List<Guid>();

                graph[relation.MainPersonId].Add(relation.AgentPersonId);
            }

            // We'll track visited nodes to detect cycles
            var visited = new HashSet<Guid>();
            var recursionStack = new HashSet<Guid>();

            foreach (var node in graph.Keys)
            {
                if (HasCycle(node, graph, visited, recursionStack))
                    return false;
            }

            return true;
        }

        private static bool HasCycle(Guid node, Dictionary<Guid, List<Guid>> graph,
            HashSet<Guid> visited, HashSet<Guid> recursionStack)
        {
            if (recursionStack.Contains(node))
                return true;

            if (visited.Contains(node))
                return false;

            visited.Add(node);
            recursionStack.Add(node);

            if (graph.TryGetValue(node, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (HasCycle(neighbor, graph, visited, recursionStack))
                        return true;
                }
            }

            recursionStack.Remove(node);
            return false;
        }
        public static bool CheckInheritorExists(ICollection<SignRequestPersonRelated> signRequestPersonRelated,Guid mainPersonId)
        {
            /*Use BFS or Floyd Algorithm for check cyclic dependency*/
            bool isValid = false;

            if (signRequestPersonRelated.Any(x=>x.MainPersonId==mainPersonId && x.AgentTypeId=="8"))
                isValid = true;

            return isValid;
        }

        public static string FindAgentsOfOriginalPerson(SignRequest SignReq, SignRequestPerson OneONotarySignPerson)
        {
            StringBuilder sb = new StringBuilder();
            string annotationText = null;
            foreach (var Person in SignReq.SignRequestPeople)
            {

                if (Person.IsRelated == "1")
                {
                    foreach (var agent in SignReq.SignRequestPersonRelateds)
                    {
                        if (agent.MainPersonId == OneONotarySignPerson.Id)
                        {

                            switch (agent.AgentTypeId)
                            {

                                #region نماینده
                                case "2":
                                    sb = new StringBuilder();
                                    annotationText += " با نمایندگی {0} {1} فرزند {2} به شماره ملی {3} و تاریخ تولد {4}   ";
                                    if (agent.AgentPerson.SexType == "2")
                                    {
                                        annotationText = string.Format(annotationText, "آقای ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                            , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    else
                                    {
                                        annotationText = string.Format(annotationText, "خانم ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                             , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }

                                    sb.Append(annotationText);
                                    break;
                                #endregion

                                #region ولی
                                case "3":
                                    sb = new StringBuilder();
                                    annotationText += " با ولایت {0} {1} فرزند {2} به شماره ملی {3} و تاریخ تولد {4}  ";
                                    if (agent.AgentPerson.SexType == "2")
                                    {
                                        annotationText = string.Format(annotationText, "آقای ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                            , agent.AgentPerson.NationalNo, agent.AgentPerson   .BirthDate);
                                    }
                                    else
                                    {
                                        annotationText = string.Format(annotationText, "خانم ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                             , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    sb.Append(annotationText);
                                    break;
                                #endregion

                                #region قیم
                                case "7":
                                    sb = new StringBuilder();
                                    annotationText += " با قیومیت {0} {1} فرزند {2} به شماره ملی {3} و تاریخ تولد {4}  ";
                                    if (agent.AgentPerson.SexType == "2")
                                    {
                                        annotationText = string.Format(annotationText, "آقای ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                            , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    else
                                    {
                                        annotationText = string.Format(annotationText, "خانم ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                             , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    sb.Append(annotationText);
                                    break;
                                #endregion

                                #region متولی
                                case "6":
                                    sb = new StringBuilder();
                                    annotationText += " با متولی {0} {1} فرزند {2} به شماره ملی {3} و تاریخ تولد {4}  ";
                                    if (agent.AgentPerson.SexType == "2")
                                    {
                                        annotationText = string.Format(annotationText, "آقای ", agent.AgentPerson.Name + " "+ agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                            , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    else
                                    {
                                        annotationText = string.Format(annotationText, "خانم ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                             , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    sb.Append(annotationText);
                                    break;
                                #endregion

                                #region قايم مقام
                                case "5":
                                    sb = new StringBuilder();
                                    annotationText += " با قائم مقامی {0} {1} فرزند {2} به شماره ملی {3} و تاریخ تولد {4}  ";
                                    if (agent.AgentPerson.SexType == "2")
                                    {
                                        annotationText = string.Format(annotationText, "آقای ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                            , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    else
                                    {
                                        annotationText = string.Format(annotationText, "خانم ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                             , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    sb.Append(annotationText);
                                    break;
                                #endregion

                                #region مدیر
                                case "4":
                                    sb = new StringBuilder();
                                    annotationText += " با مدیریت {0} {1} فرزند {2} به شماره ملی {3} و تاریخ تولد {4}  ";
                                    if (agent.AgentPerson.SexType == "2")
                                    {
                                        annotationText = string.Format(annotationText, "آقای ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                            , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    else
                                    {
                                        annotationText = string.Format(annotationText, "خانم ", agent.AgentPerson.Name + " " + agent.AgentPerson.Family, agent.AgentPerson.FatherName
                                             , agent.AgentPerson.NationalNo, agent.AgentPerson.BirthDate);
                                    }
                                    sb.Append(annotationText);
                                    break;
                                #endregion

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return annotationText;
        }

    }
}
