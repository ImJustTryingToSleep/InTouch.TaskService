CREATE OR REPLACE FUNCTION public.gettasks_bycolumn(
	_id uuid)
    RETURNS SETOF tasks 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
r tasks%rowtype;
BEGIN
FOR r IN
SELECT * FROM tasks WHERE columnid = _id AND associatedwith = '00000000-0000-0000-0000-000000000000'
    LOOP
        -- здесь возможна обработка данных
        RETURN NEXT r; -- возвращается текущая строка запроса
END LOOP;
    RETURN;
END;
$BODY$;