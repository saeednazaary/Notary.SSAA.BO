
namespace Notary.SSAA.BO.Infrastructure.Persistence.SQLQueries
{
    internal static class SignRequestQuery
    {
        public static string SignRequestSearch = @"SELECT 
    sign_request.id AS ByteId, 
    sign_request.req_no AS ReqNo, 
    sign_request.req_date AS ReqDate, 
    sign_request.national_no AS NationalNo, 
    sign_request.sign_date AS SignDate,
    sign_request_subject.title AS SignRequestSubjectTitle,
    SIGN_REQUEST_GETTER.title AS SignRequestGetterTitle,
    (
        SELECT LISTAGG(name || ' ' || family, ',')
        FROM sign_request_person
        WHERE sign_request_person.sign_request_id = sign_request.id
        AND sign_request_person.scriptorium_id = :scriptoriumId
        {0}  -- Person filters go here
    ) AS persons,
    sign_request.state AS State,
    (CASE 
        WHEN sign_request.state = 1 THEN 'تنظیم شده' 
        WHEN sign_request.state = 2 THEN 'تأیید شده' 
        WHEN sign_request.state = 3 THEN 'بی اثر شده' 
     END) AS StateText
FROM 
    sign_request
    JOIN sign_request_subject ON sign_request.sign_request_subject_id = sign_request_subject.id
    JOIN SIGN_REQUEST_GETTER ON sign_request.sign_request_getter_id = SIGN_REQUEST_GETTER.id
WHERE 
    sign_request.scriptorium_id = :scriptoriumId
    {1}  -- Your main filters go here
ORDER BY 
    sign_request.req_no DESC {2}
OFFSET :Offset ROWS FETCH NEXT :Fetch ROWS ONLY";

        public static string SignRequestSelected = @"SELECT 
    sr.id AS byteId,
    sr.req_no AS ReqNo,
    sr.national_no AS NationalNo,
    sr.req_date AS ReqDate,
    sr.sign_date AS SignDate,
    sr.state AS StateId,
    srg.title AS SignRequestGetterTitle,
    srs.title AS SignRequestSubjectTitle,
    (
        SELECT LISTAGG(p.name || ' ' || p.family, ',')
        FROM sign_request_person p
        WHERE p.sign_request_id = sr.id
    ) AS Persons
FROM 
    sign_request sr
    JOIN sign_request_getter srg ON sr.sign_request_getter_id = srg.id
    JOIN sign_request_subject srs ON sr.sign_request_subject_id = srs.id
WHERE 
    sr.id IN (:Ids)  -- Replace with your selectedItemsIds values";

        public static string SignRequestCount = @"SELECT COUNT(DISTINCT sr.id) AS TotalCount
FROM 
    sign_request sr
    JOIN sign_request_subject srs ON sr.sign_request_subject_id = srs.id
    JOIN sign_request_getter srg ON sr.sign_request_getter_id = srg.id
    LEFT JOIN sign_request_person srp ON srp.sign_request_id = sr.id
        AND srp.scriptorium_id = :scriptoriumId
        {0}  -- Person filters move here
WHERE 
    sr.scriptorium_id = :scriptoriumId
    {1}  -- Your main filters go here
    -- No EXISTS condition needed";

    }
}
