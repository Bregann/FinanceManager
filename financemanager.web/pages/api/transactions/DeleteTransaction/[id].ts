import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export default async function handler (req: NextApiRequest, res: NextApiResponse<boolean>): Promise<void> {
  if (req.method !== 'POST') {
    res.status(405)
  } else {
    const { id } = req.query

    const fetchResult = await backendFetchHelper.doPost(`/Transactions/DeleteTransaction/${id?.toString()}`, null)
    console.log(fetchResult)
    if (fetchResult.errored || fetchResult.data === false) {
      res.status(500).json(false)
    } else {
      res.status(200).json(fetchResult.data as boolean)
    }
  }
}
