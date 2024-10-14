CREATE OR REPLACE FUNCTION public.get_board(
	_id uuid)
    RETURNS SETOF boards 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
r boards%rowtype;
BEGIN
FOR r IN
SELECT * FROM boards WHERE id = _id
    LOOP
                           -- здесь возможна обработка данных
    RETURN NEXT r; -- возвращается текущая строка запроса
END LOOP;
    RETURN;
END;
$BODY$;