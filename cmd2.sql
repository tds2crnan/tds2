affichage Semaine/Sub

	recuperation Cycle disponible pour la sub sur une periode donnee:
	
		select *
		from cycles 
		where idSub = 1 and 
		( 
				(dateDebut < "2013-12-01" and dateFin > "2013-12-01") 
			or 	(dateDebut < "2013-12-01" and dateFin > "2013-12-01") 
		);

	recuperation des equip d un sub pour:
	select * 
	from equips left join , ce on (equips.id = ce.idEquip) left join cycles on (cd.idCycle= cycles.id)
	where 
			(cycles.dateDebut < "2013-12-01" and cycles.dateFin > "2013-12-01") 
		or 	(cycles.dateDebut < "2013-12-01" and cycles.dateFin > "2013-12-01") 

	select *
	from cycles ct types vacations
	where
		cycles.idSub = 1 and
		
		(	(cycles.dateDebut < "2013-12-01" and cycles.dateFin > "2013-12-01")
		or 	(cycles.dateDebut < "2013-12-01" and cycles.dateFin > "2013-12-01") 
		)

	liste agent disponible pour un remplacement pour une vaction et une date donnee:
	select
		agents.id as id, agents.nom as nom 
	from
		agents left join carrieres on( agent.id = carrieres.idAgent) left join subs on (carrieres.idSub = subs.id) left join cycles on ( subs.id = cycles.idSub) left join types on ( cycles.id = types.idCycle)
	where
		types.id = 0 and
		(	(carrieres.dateDebut < "2013-12-01" and carrieres.dateFin > "2013-12-01")
		or 	(carrieres.dateDebut < "2013-12-01" and carrieres.dateFin > "2013-12-01") 
		)	
	;
	
	liste des equipe disponible pour une sub et une date donnee:
	
	select 
		equips.id as id, equips.nom as nom
	from
		equips	left join ce on ( equips.id = ce.idEquip )
				left join cycles on ( ce.idCycle = cycles.id )
	where
		cycles.idSub = 1 and
		(	(cycles.dateDebut < "2013-12-01" and cycles.dateFin > "2013-12-01") or
		 	(cycles.dateDebut < "2013-12-01" and cycles.dateFin > "2013-12-01") 
		)	
	;
	
	