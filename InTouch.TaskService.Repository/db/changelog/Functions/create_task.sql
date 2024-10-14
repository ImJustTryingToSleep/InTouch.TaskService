CREATE OR REPLACE FUNCTION public.create_task(
	_name character,
	_description character,
	_enddate timestamp without time zone,
	_author uuid,
	_executors uuid[],
	_associatedwith uuid,
	_columnid uuid)
    RETURNS SETOF uuid 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    _id uuid;
BEGIN
    INSERT INTO public.tasks(name, description, enddate, author, executors, createdat, updatedat, columnid, associatedwith) 
	VALUES(_name, _description, _enddate, _author, _executors, NOW(), NOW(), _columnid, _associatedwith) RETURNING id INTO _id;
    RETURN NEXT _id;
END;
$BODY$;