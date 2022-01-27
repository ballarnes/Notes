import React, { useEffect } from 'react'
import { Col, Container, Row, Spinner } from 'react-bootstrap'
import Pagination from '../../components/Pagination'
import { observer } from 'mobx-react'
import { useInjection } from '../../ioc/ioc.react'
import ownTypes from '../../ioc/ownTypes'
import NoteCard from '../../components/NoteCard'
import NotesStore from '../../stores/NotesStore'

const Notes = observer(() => {
  const store = useInjection<NotesStore>(ownTypes.notesStore);

  useEffect(() => {
    const getNote = async () => {
      await store.init();
    }
    getNote()
  }, [store])

  return (
    <Container>
      <Row className="justify-content-center">
        {store.isLoading ? (
          <Spinner animation="border" />
        ) : (
          <>
            {store.notes?.map((note, key) => (
              <Col key={key} sm={6} md={4} lg={3} xl={2} className="mb-5 mt-2">
                <NoteCard note={note} />
              </Col>
            ))}
          </>
        )}

      </Row>
      <Pagination total={store.totalPages} active={store.currentPage} onChange={(val) => { store.changePage(val) }}/>
    </Container>
  )
});

export default Notes
