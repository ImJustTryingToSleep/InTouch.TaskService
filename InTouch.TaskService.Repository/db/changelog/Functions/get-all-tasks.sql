CREATE OR REPLACE FUNCTION public.get_all_tasks(
	)
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
SELECT * FROM tasks
                  LOOP
              -- здесь возможна обработка данных
    RETURN NEXT r; -- возвращается текущая строка запроса
END LOOP;
    RETURN;
END;
$BODY$