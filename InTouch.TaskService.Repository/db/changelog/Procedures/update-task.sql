CREATE OR REPLACE PROCEDURE public.update_task(
	_columnid uuid,
	_name character,
	_description character,
	_executors uuid[],
	_enddate timestamp without time zone,
	_taskid uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
UPDATE public.tasks
SET columnid = _columnid, name = _name, description = _description, executors = _executors, updatedat = NOW(), enddate = _enddate
WHERE id = _taskid;
END;
$BODY$;