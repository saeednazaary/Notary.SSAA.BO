
namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class LambdaString<TItemProperties, TSearchData>
    {

        /// <summary>
        /// متد جنرالی که  برای ساخت شرط فیلتر لوکاپ یا گرید ساخته شده
        /// نکته: این متد برای فیلدهایی که لیست می باشند ساخته نشده است، برای ساخت فیلدهای لیستی از اکستنشن متد(1) استفاده کنید
        /// 1:AddLambdaQueryForEntityFieldList
        /// </summary>
        /// <param name="lookupFilterItem">انتیتی مد نظر که فیلتر  هم نام پراپرتی آن خورده می شود</param>
        /// <param name="gridSearchInput"> فیلترهای مخصوص هر فیلد </param>
        /// <param name="globalSearch"> فیلتر کلی بر روی تمام فیلدها</param>
        /// <param name="FieldsNotInFilterSearch">لیستی از فیلدهایی که از انتیتی مدنظر که نمیخواهیم بر روی آنها فیلتر بزنیم </param>        /// <returns></returns>
        public static string CreateWhereLambdaString(TItemProperties lookupFilterItem, ICollection<TSearchData> gridSearchInput, string globalSearch, List<string> FieldsNotInFilterSearch)
        {

            //   SignRequestGetterSelectableLookupItem lookupFilterItem = new();
            string filterQueryString;
            List<string> filterQueryList = new List<string>();


            foreach (var item in gridSearchInput)
            {
                string Filter = item.GetType().GetProperty("Filter").GetValue(item, null).ToString();
                string Value = item.GetType().GetProperty("Value").GetValue(item, null).ToString();
                if (!string.IsNullOrEmpty(Value))
                {
                    if (!FieldsNotInFilterSearch.Contains(Filter.ToLower()))
                    {
                        if (filterQueryList.Count == 0)
                        {
                            filterQueryList.Add($" {Filter}.Contains(\"{Value}\") ");

                        }
                        else
                        {
                            filterQueryList.Add($" && {Filter}.Contains(\"{Value}\") ");
                        }

                    }

                }
            }
            if (!string.IsNullOrEmpty(globalSearch))
            {
                globalSearch = globalSearch.PersianToArabic();
                if (filterQueryList.Count == 0)
                {
                    int propCount = lookupFilterItem.GetPropertyValues().Where(p => !FieldsNotInFilterSearch.Contains(p.Key.ToLower())).ToList().Count;
                    var lookupFilterItemGetPropertyValues = lookupFilterItem.GetPropertyValues().Where(p => !FieldsNotInFilterSearch.Contains(p.Key.ToLower()));
                    foreach (var item in lookupFilterItemGetPropertyValues)
                    {

                        if (filterQueryList.Count == 0)
                        {
                            filterQueryList.Add($" {item.Key}.Contains(\"{globalSearch}\") ");

                        }
                        else
                        {
                            filterQueryList.Add($" || {item.Key}.Contains(\"{globalSearch}\") ");
                        }

                    }
                }
                else
                {
                    int propCount = lookupFilterItem.GetPropertyValues().Where(p => !FieldsNotInFilterSearch.Contains(p.Key.ToLower())).ToList().Count;
                    var lookupFilterItemGetPropertyValues = lookupFilterItem.GetPropertyValues().Where(p => !FieldsNotInFilterSearch.Contains(p.Key.ToLower()));
                    int index = 1;
                    foreach (var item in lookupFilterItemGetPropertyValues)
                    {

                        if (index == 1)
                        {
                            filterQueryList.Add($" && ( {item.Key}.Contains(\"{globalSearch}\") ");
                        }
                        else
                        {
                            filterQueryList.Add($" || {item.Key}.Contains(\"{globalSearch}\") ");
                        }
                        if (propCount == index)
                        {
                            filterQueryList.Add(" )");
                        }

                        index++;
                    }
                }

            }
            filterQueryString = string.Join(" ", filterQueryList);
            return filterQueryString;
        }

    }
}
