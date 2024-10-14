CREATE OR REPLACE PROCEDURE public.update_board(
	_boardid uuid,
	_name character)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
UPDATE public.boards
SET  name = _name
WHERE id = _boardid;
END;
$BODY$;