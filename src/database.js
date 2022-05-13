import PouchDB from 'pouchdb-browser';

/**
 * @type {TextDatabase}
 */
const esTxtDB = new PouchDB("es-texts");


/**
 * 
 * @returns {Promise<[EsText[], number]>}
 */
export async function getTexts() {
    /**
     * @type {AllTextsResponse}
     */
    const docs = await esTxtDB.allDocs({ include_docs: true });
    /**
     * @type {EsText[]}
     */
    const texts = docs.rows.reduce((curr, next) => {
        if (next?.doc) { curr.push(next.doc); }
        return curr;
    }, []);
    return [texts, docs.total_rows];
}