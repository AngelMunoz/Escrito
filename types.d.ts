type EsText = {
  _id: string;
  _rev: string;
  text: string;
  holes: string[];
};

type TextDatabase = PouchDB.Database<EsText>;
type AllTextsResponse = PouchDB.Core.AllDocsResponse<EsText>;
