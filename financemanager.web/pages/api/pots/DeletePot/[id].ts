import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export default async function handler (req: NextApiRequest, res: NextApiResponse<boolean>): Promise<void> {
  if (req.method !== 'DELETE') {
    res.status(405)
  }

  const { id } = req.query

  const fetchResult = await backendFetchHelper.doDelete(`/Pots/DeletePot/${id?.toString()}`)

  if (fetchResult.errored) {
    res.status(500)
  } else {
    res.status(200).json(fetchResult.data as boolean)
  }
}
