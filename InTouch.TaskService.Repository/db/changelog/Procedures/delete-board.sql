CREATE OR REPLACE PROCEDURE public.delete_board(
	_boardid uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
DELETE FROM public.boards WHERE id = _boardid;
END;
$BODY$;