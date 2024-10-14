CREATE OR REPLACE PROCEDURE public.delete_task(
	_id uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
DELETE FROM public.tasks
WHERE id = _id OR associatedwith = _id;
END;
$BODY$;