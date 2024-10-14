CREATE OR REPLACE FUNCTION public.get_column(
	_id uuid)
    RETURNS SETOF columns 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
r columns%rowtype;
BEGIN
FOR r IN
SELECT * FROM columns WHERE id = _id
    LOOP
                            -- здесь возможна обработка данных
    RETURN NEXT r; -- возвращается текущая строка запроса
END LOOP;
    RETURN;
END;
$BODY$;