CREATE OR REPLACE PROCEDURE public.post_board(
	_name character,
	_author uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
INSERT INTO public.boards(name, author) VALUES(_name, _author);
INSERT INTO public.columns(name, boardid)
VALUES('ToDo', (SELECT id FROM public.boards WHERE name = _name));
INSERT INTO public.columns(name, boardid)
VALUES('InProgress', (SELECT id FROM public.boards WHERE name = _name));
INSERT INTO public.columns(name, boardid)
VALUES('Done', (SELECT id FROM public.boards WHERE name = _name));
END;
$BODY$;