export interface Article {
  id: number,
  title: string,
  publishedDate: number,
  shortDescription?: string,
  originalUrl?: string,
  text?: string,
  sourceId: number
}
