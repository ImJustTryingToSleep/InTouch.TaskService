CREATE OR REPLACE PROCEDURE public.update_column(
	_name character,
	_columnid uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
UPDATE public.columns
SET name = _name
WHERE id = _columnid;
END;
$BODY$;