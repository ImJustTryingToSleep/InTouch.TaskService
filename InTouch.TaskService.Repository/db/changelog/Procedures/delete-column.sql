CREATE OR REPLACE PROCEDURE public.delete_column(
	_id uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
DELETE FROM public.columns WHERE id = _id;
END;
$BODY$;