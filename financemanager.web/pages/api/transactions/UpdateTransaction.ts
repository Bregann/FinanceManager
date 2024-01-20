import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export default async function handler (
  req: NextApiRequest,
  res: NextApiResponse<boolean>): Promise<void> {
  if (req.method !== 'POST') {
    res.status(405)
  } else {
    const fetchResult = await backendFetchHelper.doPost('/Transactions/UpdateTransaction', req.body)

    if (fetchResult.errored || fetchResult.data === false) {
      res.status(500)
    } else {
      res.status(200).json(fetchResult.data as boolean)
    }
  }
}
