CREATE OR REPLACE PROCEDURE public.post_column(
	_name character,
	_boardid uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
INSERT INTO public.columns(name, boardid)
VALUES(_name, _boardid);
END;
$BODY$;