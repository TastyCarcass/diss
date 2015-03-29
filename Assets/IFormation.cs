using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface IFormation
{
	void Import (bool asNew, List<FormationModel.positionData> aList);
	
	List<FormationModel.positionData> Export (bool asNew);

	void UpdateFormationUnit(int ID, Vector3 LocalPos);

	void NodePressed(int ID);
}