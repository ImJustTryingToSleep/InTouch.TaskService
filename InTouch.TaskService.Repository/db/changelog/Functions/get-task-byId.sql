CREATE OR REPLACE FUNCTION public.get_task(
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
FOR r IN SELECT * FROM tasks WHERE id = _id
		LOOP
                                   -- здесь возможна обработка данных
    RETURN NEXT r; -- возвращается текущая строка запроса
END LOOP;
    RETURN;
END;
$BODY$;