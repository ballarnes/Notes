import React from 'react'
import { Card } from 'react-bootstrap'

interface Props {
  note: {
    title: string,
    description: string,
    author: string
  } | null
}

const NoteCard = (props: Props) => {
  if (!props.note) {
    return null
  }
  const { title, description, author } = props.note

  return (
    <Card border="dark" style={{ height: '12rem' }}>
      <Card.Header>{title}</Card.Header>
      <Card.Body>
        <Card.Text className="text-center">
          {description}
        </Card.Text>
      </Card.Body>
      <Card.Footer className="text-muted">{author}</Card.Footer>
    </Card>
  )
}

export default NoteCard
